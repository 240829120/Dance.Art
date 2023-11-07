/**
 * 连接状态
 */
const ConnectionStatus = {

    /**
     * 断开
     */
    Disconnected: "Disconnected",

    /**
     * 等待
     */
    Waiting: "Waiting",

    /**
     * 已经连接
     */
    Connected: "Connected"
};

/**
 * 连接分组
 */
class ConnectionGroupModel {

    /**
     * 连接分组
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
    /* name */

    /**
     * 分组名称
     * @returns {string}
     */
    get name() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Name;
    }
}

/**
 * 连接模型
 */
class ConnectionModel {

    /**
     * 连接模型
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
    /* ID */

    /**
     * 获取编号
     * @returns {string}
     */
    get id() {
        return this.HOST_OBJECT.ID;
    }

    /* ----------------------------------------------------- */
    /* name */

    /**
     * 获取名称
     * @returns {string}
     */
    get name() {
        return this.HOST_OBJECT.Name;
    }

    /* ----------------------------------------------------- */
    /* description */

    /**
     * 获取描述
     * @returns {string}
     */
    get description() {
        return this.HOST_OBJECT.Description;
    }

    /* ----------------------------------------------------- */
    /* status */

    /**
     * 获取状态
     * @returns {ConnectionStatus}
     */
    get status() {
        return this.HOST_OBJECT.Status.ToString();
    }
}

/**
 * 连接源接收数据事件参数
 */
class ConnectionSourceReceiveDataEventArgs {

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
    /* data */

    /**
     * 获取二进制数据
     * @returns {Int8Array}
     */
    get data() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return new Int8Array(this.HOST_OBJECT.Data);
    }

    /* ----------------------------------------------------- */
    /* stringData */

    /**
     * 获取字符串数据
     * @returns {string}
     */
    get stringData() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.StringData;
    }
}

/**
 * 连接服务
 */
class ConnectionService {

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
        this.NAME = "ConnectionService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 根据ID获取连接模型
     * @param {string} id 编号
     * @returns {ConnectionModel} 连接模型
     */
    getConnectionByID(id) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            retun;

        let result = this.HOST_OBJECT.GetConnectionByID(`${id}`);

        return new ConnectionModel(result);
    }

    /**
     * 根据名称获取连接模型
     * @param {string} name 名称
     * @returns {ConnectionModel} 连接模型
     */
    getConnectionByName(name) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        let result = this.HOST_OBJECT.GetConnectionByName(`${name}`);

        return new ConnectionModel(result);
    }

    /**
     * 根据连接分组名获取连接集合
     * @param {string} groupName 连接分组名
     * @returns {ConnectionModel[]} 连接模型集合
     */
    getConnectionsByGroupName(groupName) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        let array = this.HOST_OBJECT.GetConnectionsByGroupName(groupName);
        if (array === null || array === undefined)
            return null;

        let result = new Array();
        for (var i = 0; i < array.Length; i++) {
            let model = new ConnectionModel(array[i]);
            result.push(model);
        }

        return result;
    }

    /**
     * 获取连接分组
     * @returns {ConnectionGroupModel[]} 连接分组模型集合
     */
    getConnectionGroups() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        let array = this.HOST_OBJECT.GetConnectionGroups();
        if (array === null || array === undefined)
            return null;

        let result = new Array();
        for (var i = 0; i < array.Length; i++) {
            let group = new ConnectionGroupModel(array[i]);
            result.push(group);
        }

        return result;
    }
}

export { ConnectionStatus, ConnectionModel, ConnectionSourceReceiveDataEventArgs, ConnectionService }