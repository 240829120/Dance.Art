import { ControlGridItemModelBaseWrapper } from './index'

/**
 * 命令按钮
 */
class CommandButtonWrapper extends ControlGridItemModelBaseWrapper {

    /**
     * 命令按钮
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
    /* deviceNames */

    /**
     * 获取设备名称集合，使用','分隔
     * @returns {string}
     */
    get deviceNames() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.DeviceNames;
    }

    /**
     * 设置设备名称集合，使用','分隔
     * @returns {string}
     */
    set deviceNames(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.DeviceNames = value;
    }

    /* ----------------------------------------------------- */
    /* command */

    /**
     * 获取设备名称集合，使用','分隔
     * @returns {string}
     */
    get command() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Command;
    }

    /**
     * 设置设备名称集合，使用','分隔
     * @returns {string}
     */
    set command(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Command = value;
    }
}

export { CommandButtonWrapper }