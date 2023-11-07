import { ConnectionStatus, ConnectionModel } from './index'

/**
 * Ping连接源
 */
class PingConnectionSource {

    /**
     * Ping连接源
     * @param {ConnectionModel} model 连接模型
     */
    constructor(model) {

        /**
         * 宿主对象
         */
        this.ConnectionModel = model;
    }
}

export { PingConnectionSource }