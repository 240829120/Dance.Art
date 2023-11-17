import { ControlGridItemModelBaseWrapper } from './index'

/**
 * 脚本按钮
 */
class ScriptButtonWrapper extends ControlGridItemModelBaseWrapper {

    /**
     * 脚本按钮
     * @param {any} host 宿主对象
     */
    constructor(host) {
        super(host);
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* content */

    /**
     * 获取内容
     * @returns {string}
     */
    get content() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Content;
    }

    /**
     * 设置行
     * @returns {string}
     */
    set content(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Content = value;
    }

    /* ----------------------------------------------------- */
    /* onClick */

    /**
     * 获取点击执行的脚本
     * @returns {string}
     */
    get onClick() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.DeviceNames;
    }

    /**
     * 设置点击执行的脚本
     * @returns {string}
     */
    set onClick(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.DeviceNames = value;
    }
}

export { ScriptButtonWrapper }