import { ResourceElementModelWrapper } from '../Resource/index'

/**
 * 控制面板项包装器基类
 */
class ControlGridItemModelBaseWrapper extends ResourceElementModelWrapper {

    /**
     * 控制面板项包装器基类
     * @param {any} host 宿主对象
     */
    constructor(host) {
        super(host);
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* row */

    /**
     * 获取行
     * @returns {number}
     */
    get row() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Row;
    }

    /**
     * 设置行
     * @returns {number}
     */
    set row(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Row = value;
    }

    /* ----------------------------------------------------- */
    /* column */

    /**
     * 获取列
     * @returns {number}
     */
    get column() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Column;
    }

    /**
     * 设置列
     * @returns {number}
     */
    set column(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Column = value;
    }
}

/**
 * 控制面板脚本服务
 */
class ControlGridScriptServiceWrapper {
    /**
     * 消息服务
     */
    constructor() {

        /**
         * 服务命名空间
         */
        this.NAME_SPACE = "DANCE_ART_SCRIPT";

        /**
         * 服务名称
         */
        this.NAME = "ControlGridScriptService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 获取控制面板文档, 必须先打开该文档才能获取
     * @param {string} path 文档相对路径或绝对路径
     * @param {string} id 控件ID
     * @returns {any} 控件Host
     */
    getControlGridItem(path, id) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        let host = this.HOST_OBJECT.GetControlGridItem(path, id);
        return host;
    }
}

export { ControlGridItemModelBaseWrapper, ControlGridScriptServiceWrapper }