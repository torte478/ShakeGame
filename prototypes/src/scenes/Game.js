import Phaser from '../lib/phaser.js';

import Animation from '../game/utils/Animation.js';
import Utils from '../game/utils/Utils.js';

import Core from '../game/Core.js';

export default class Game extends Phaser.Scene {

	/** @type {Core} */
	_core;

    constructor() {
        super('game');
    }

    preload() {
        const me = this;

        Utils.loadSpriteSheet(me, 'persons', 50);
        Utils.loadSpriteSheet(me, 'items', 25);
        
        Utils.loadImage(me, 'back');
        Utils.loadImage(me, 'fade');
    }

    create() {
        const me = this;

        Animation.init(me);

        me._core = new Core(me);
    }

    update() {
        const me = this;

        me._core.update();
    }
}