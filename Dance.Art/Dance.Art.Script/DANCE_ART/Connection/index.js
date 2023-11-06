/**
 * 连接源
 */
class ConnectionSource {

}

/**
 * 连接模型
 */
class ConnectionModel {

    /**
     * 连接模型
     */
    constructor() {
        /**
         * 编号
         */
        this.ID = null;

        /**
         * 名称
         */
        this.Name = null;

        /**
         * 状态
         */
        this.Status = null;

        /**
         * {ConnectionSource} 状态
         */
        this.Source = new ConnectionService();
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

        return result;
    }
}

export { ConnectionSource, ConnectionModel, ConnectionService }