import Phaser from '../lib/phaser.js';

import Audio from './utils/Audio.js';
import Button from './utils/Button.js';
import ButtonConfig from './utils/ButtonConfig.js';
import Utils from './utils/Utils.js';

import Config from './Config.js';
import Consts from './Consts.js';
import Enemy from './Enemy.js';

export default class Core {

    /** @type {Phaser.Scene} */
    _scene;

    /** @type {Audio} */
    _audio;

    /** @type {Phaser.GameObjects.Text} */
    _log;

    /** @type {Number} */
    _currentGun;

    /** @type {Phaser.GameObjects.Image} */
    _fade;

    /** @type {Phaser.Physics.Arcade.Group} */
    _bullets;

    /** @type {Phaser.GameObjects.Image} */
    _player;

    /** @type {Enemy[]} */
    _enemies;

    /** @type {Phaser.Physics.Arcade.Group} */
    _enemyBullets;

    /**
     * @param {Phaser.Scene} scene 
     */
    constructor(scene) {
        const me = this;

        me._scene = scene;
        me._audio = new Audio(scene);

        const mainCamera = scene.cameras.main;
        mainCamera.setScroll(mainCamera.width / -2, mainCamera.height / -2)

        scene.add.image(0, 0, 'back')
            .setDepth(-1000);

        me._player = scene.physics.add.image(0, mainCamera.height / 3, 'persons', 0);
        me._currentGun = -1;

        me._fade = scene.add.image(-250, 0, 'fade')
            .setDepth(1000)
            .setVisible(false);

        scene.physics.world.setBounds(-500, -400, 1000, 800);
        me._bullets = scene.physics.add.group({ collideWorldBounds: true });
        me._enemyBullets = scene.physics.add.group();

        scene.physics.world.on('worldbounds', (body) => {
            me._bullets.killAndHide(body.gameObject);
        });

        const enemyGroup = scene.physics.add.group();
        me._enemies = [];
        for (let i = 0; i < 20; ++i) {
            const type = Phaser.Math.Between(0, 2);
            me._enemies.push(new Enemy(scene, enemyGroup, type, Utils.toPoint(me._player), me._enemyBullets));
        }

        for (let i = 0; i < 10; ++i) {
            const friend = scene.physics.add.image(0, 0, 'persons', 4)
                .setDepth(100);
            
            const zone = new Phaser.Geom.Rectangle(-450, -350, 900, 550);

            const start = Phaser.Geom.Rectangle.Random(zone);
            const finish = Phaser.Geom.Rectangle.Random(zone);

            const duration = Utils.getTweenDuration(start, finish, 100);

            friend.setPosition(start.x, start.y);
            me._tween = scene.add.tween({
                targets: friend,
                x: finish.x,
                y: finish.y,
                yoyo: true,
                repeat: -1,
                duration: duration
            });
        }

        scene.physics.add.collider(
            me._bullets,
            enemyGroup,
            (bullet, enemy) => {
                me._bullets.killAndHide(bullet);
                /** @type {Enemy} */
                const owner = enemy.body.owner;
                owner.onPlayerShot();
            }
        );

        scene.physics.add.collider(
            me._player,
            enemyGroup,
            (p, e) => {
                console.log('game over - hit');
                return me._scene.scene.start('game');
            }
        );

        scene.physics.add.collider(
            me._player,
            me._enemyBullets,
            (p, b) => {
                console.log('game over - bullet');
                return me._scene.scene.start('game');
            }
        );

        scene.input.on('pointerdown', me._onPointerDown, me);

        Utils.ifDebug(Config.Debug.ShowSceneLog, () => {
            me._log = scene.add.text(10, 10, '', { fontSize: 14, backgroundColor: '#000' })
                .setScrollFactor(0)
                .setDepth(Consts.Depth.Max);
        });
    }

    update() {
        const me = this;

        Utils.ifDebug(Config.Debug.ShowSceneLog, () => {
            let text = 
                `mse: ${me._scene.input.activePointer.worldX | 0} ${me._scene.input.activePointer.worldY | 0}\n` +
                `gun: ${me._currentGun}\n` +
                `blt: ${Config.UseBullets}`;

            me._log.setText(text);
        });
    }

    /**
     * @param {Phaser.Input.Pointer} pointer 
     */
    _onPointerDown(pointer) {
        const me = this;

        const canShot = (pointer.worldX < -50 && me._currentGun === -1)
                        || (pointer.worldX >= -50 && pointer.worldX <= 50)
                        || (pointer.worldX > 50 && me._currentGun === 1);

        if (!canShot) {
            me._fade
                .setPosition(-250 * me._currentGun, 0)
                .setVisible(true);
            return;
        }

        me._fade.setVisible(false);

        if (Config.UseBullets) {
            /** @type {Phaser.Physics.Arcade.Image} */
            const bullet = me._bullets.create(me._player.x, me._player.y, 'items', 1);
            bullet.setDepth(100);

            const angle = Phaser.Math.Angle.BetweenPoints(bullet, Utils.buildPoint(pointer.worldX, pointer.worldY));
            me._scene.physics.velocityFromAngle(
                Phaser.Math.RadToDeg(angle), 
                800, 
                bullet.body.velocity);
            bullet.body.onWorldBounds = true;

        } else {

            const bodies = me._scene.physics.overlapCirc(pointer.worldX, pointer.worldY, 3);
            for (let i = 0; i < bodies.length; ++i) {
                /** @type {Enemy} */
                const enemy = bodies[i].owner;
                const bulletOwner = bodies[i].bulletOwner;

                if (!!bulletOwner) {
                    /** @type {Phaser.Physics.Arcade.Image} */
                    const bullet = bodies[i].gameObject;

                    bullet.setVelocity(0);
                    me._enemyBullets.killAndHide(bullet);
                }
                else if (!!enemy)
                    enemy.onPlayerShot();
                else {
                    console.log('friendly fire');
                    return me._scene.scene.start('game');
                }
            }

            if (Utils.all(me._enemies, e => !e._container.visible)) {
                for (let i = 0; i < me._enemies.length; ++i)
                    me._enemies[i].reset();
            }
        }   

        me._currentGun *= -1;
    }
}