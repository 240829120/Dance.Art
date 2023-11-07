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
 * 连接对象
 */
class ConnectionModel {

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

    /// <summary>
    /// 根据ID获取连接模型
    /// </summary>
    /// <param name="id">编号</param>
    /// <returns>连接模型</returns>
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
}

export { ConnectionStatus, ConnectionModel, ConnectionService }