/**
 * 设备接收数据事件参数包装器
 */
class DeviceReceiveBufferDataEventArgsWrapper {

    /**
     * 连接对象
     * @param {any} host 宿主对象
     */
    constructor(host) {

        /**
         * 宿主对象
         */
        this.HOST_OBJECT = host;
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* buffer */

    /**
     * 获取二进制数据
     * @returns {Int8Array}
     */
    get buffer() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return new Int8Array(this.HOST_OBJECT.Buffer);
    }

    /* ----------------------------------------------------- */
    /* length */

    /**
     * 获取二进制数据长度
     * @returns {number}
     */
    get length() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Length;
    }

    /**
     * 获取数据字符串(UTF-8)
     * @returns {string}
     */
    get bufferString() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.GetBufferString();
    }
}

/**
 * 设备源模型包装器基类
 */
class DeviceSourceModelWrapperBase {

    /**
     * 设备源模型包装器基类
     * @param {any} HOST_OBJECT 宿主对象
     */
    constructor(HOST_OBJECT) {

        /**
         * 宿主对象
         */
        this.HOST_OBJECT = HOST_OBJECT;
    }

    /**
     * 连接
     */
    connect() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return;

        this.HOST_OBJECT.Connect();
    }

    /**
     * 断开
     */
    disconnect() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return;

        this.HOST_OBJECT.Disconnect();
    }
}

/**
 * 设备脚本服务包装器
 */
class DeviceScriptServiceWrapper {

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
        this.NAME = "DeviceScriptService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 获取设备源
     * @param {string} name 名称
     * @returns {any} 设备源 HOST_OBJECT
     */
    getDeviceSource(name) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            retun;

        let result = this.HOST_OBJECT.GetDeviceSource(`${name}`);

        return result;
    }
}

export { DeviceReceiveBufferDataEventArgsWrapper, DeviceSourceModelWrapperBase, DeviceScriptServiceWrapper }