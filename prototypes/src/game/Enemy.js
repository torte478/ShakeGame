import Phaser from '../lib/phaser.js';
import Config from './Config.js';
import Utils from './utils/Utils.js';

export default class Enemy {

    /** @type {Phaser.Scene} */
    _scene;

    /** @type {Phaser.Physics.Arcade.Image} */
    _image;

    /** @type {Phaser.Tweens.Tween} */
    _tween;

    /** @type {Phaser.GameObjects.Container} */
    _сontainer;

    /** @type {Phaser.GameObjects.Text} */
    _text;

    /** @type {Number} */
    _hp;

    /** @type {Number} */
    _type;

    /** @type {Phaser.Geom.Point} */
    _playerPos;

    /** @type {Phaser.Physics.Arcade.Group} */
    _bullets;

    /** @type {Number} */
    _tweenIndex;

    /** @type {Boolean} */
    _useMelleeAttack;

    /** @type {Boolean} */
    _useRemoteAttack;
    
    /**
     * @param {Phaser.Scene} scene 
     * @param {Phaser.Physics.Arcade.Group} group
     * @param {Number} type;
     * @param {Phaser.Geom.Point} playerPos
     * @param {Phaser.Physics.Arcade.Group} bullets
     */
    constructor(scene, group, type, playerPos, bullets) {
        const me = this;

        me._scene = scene;
        me._type = type;
        me._bullets = bullets;
        me._tweenIndex = 0;

        const flag = Phaser.Math.Between(0, 1);
        me._useMelleeAttack = Config.UseMeleeAtack && flag === 0 && type !== 2;
        me._useRemoteAttack = type === 2 || (Config.UseRemoteAtack && flag === 1);

        me._resetHP();

        const frame = type + 1;

        me._image = group.create(0, 0, 'persons', frame);
        me._text = scene.add.text(0, 0, me._hp, { color: '#ffffff', fontSize: '28px' })
            .setOrigin(0.5, 0.5)
            .setVisible(type == 1);

        me._container = scene.add.container(0, 0, [ me._image, me._text ]);

        me._image.body.owner = me;
        me._playerPos = playerPos;

        me.reset();
    }

    onPlayerShot() {
        const me = this;

        me._hp -= 1;
        if (me._hp <= 0)
            me._die();
            
        me._text.setText(me._hp);
    }

    reset() {
        const me = this;

        me._resetHP();
        me._text.setText(me._hp);
        me._tweenIndex = 0;

        if (!!me._tween)
            me._tween.pause();

        me._container.setVisible(true);

        const zone = new Phaser.Geom.Rectangle(-450, -350, 900, 250);

        const start = Phaser.Geom.Rectangle.Random(zone);
        const finish = Phaser.Geom.Rectangle.Random(zone);

        const min = me._type == 2 ? 300 : 100;
        const max = me._type == 2 ? 400 : 200;
        const speed = Phaser.Math.Between(min, max);

        const duration = Utils.getTweenDuration(start, finish, speed);

        const repeat = me._useMelleeAttack
            ? Phaser.Math.Between(1, 3)
            : -1;

        const attackIndex = Phaser.Math.Between(0, 19);

        me._container.setPosition(start.x, start.y);
        me._tween = me._scene.add.tween({
            targets: me._container,
            x: finish.x,
            y: finish.y,
            yoyo: true,
            repeat: repeat,
            duration: duration,

            onRepeat: () => {
                if (!me._useRemoteAttack)
                    return;

                me._tweenIndex = (me._tweenIndex + 1) % 20;
                if (me._tweenIndex != attackIndex)
                    return;

                /** @type {Phaser.Physics.Arcade.Image} */
                const bullet = me._bullets.create(me._container.x, me._container.y, 'items', 0);
                bullet.setDepth(100000);
                bullet.body.bulletOwner = me;

                me._scene.physics.moveTo(bullet, me._playerPos.x, me._playerPos.y, 75);
            },

            onComplete: () => {
                if (!me._useMelleeAttack)
                    return;

                me._tween = me._scene.add.tween({
                    targets: me._container,
                    x: me._playerPos.x,
                    y: me._playerPos.y,
                    duration: Utils.getTweenDuration(Utils.toPoint(me._container), me._playerPos, speed)
                });
            }
        });
    }

    _resetHP() {
        const me = this;

        me._hp = me._type === 1
            ? 3
            : 0;
    }

    _die() {
        const me = this;

        if (!!me._tween)
            me._tween.pause();

        me._container.setVisible(false);
        me._container.setPosition(0, -500);
    }
}