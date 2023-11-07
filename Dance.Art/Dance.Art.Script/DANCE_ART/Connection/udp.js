import { ConnectionStatus, ConnectionModel, ConnectionSourceReceiveDataEventArgs } from './index'

/**
 * UDP连接源
 */
class UdpConnectionSource {

    /**
     * UDP连接源
     * @param {ConnectionModel} model 连接模型
     */
    constructor(model) {

        /**
         * 连接模型
         */
        this.ConnectionModel = model;
    }

    /**
     * 接收数据时触发
     * @param {Function} func
     * @returns {boolean} 是否连接成功
     */
    onReceiveData(func) {
        if (this.ConnectionModel === null || this.ConnectionModel === undefined)
            return false;

        if (this.ConnectionModel.HOST_OBJECT === null || this.ConnectionModel.HOST_OBJECT === undefined)
            return false;

        if (this.ConnectionModel.HOST_OBJECT.Source === null || this.ConnectionModel.HOST_OBJECT.Source === undefined)
            return false;

        if (this.ConnectionModel.HOST_OBJECT.Source.ReceiveData === null || this.ConnectionModel.HOST_OBJECT.Source.ReceiveData === undefined)
            return false;

        this.ConnectionModel.HOST_OBJECT.Source.ReceiveData.connect((s, e) => {
            let args = new ConnectionSourceReceiveDataEventArgs(e);
            func(s, args);
        });

        return true;
    }

    /**
     * 发送数据
     * @param {Int8Array} data 二进制数据
     * @returns {boolean} 是否成功发送
     */
    send(data) {
        if (this.ConnectionModel === null || this.ConnectionModel === undefined)
            return false;

        if (this.ConnectionModel.status != ConnectionStatus.Connected)
            return false;

        if (this.ConnectionModel.HOST_OBJECT === null || this.ConnectionModel.HOST_OBJECT === undefined)
            return false;

        if (this.ConnectionModel.HOST_OBJECT.Source === null || this.ConnectionModel.HOST_OBJECT.Source === undefined)
            return false;

        this.ConnectionModel.HOST_OBJECT.Source.Send(data.buffer);

        return true;
    }

    /**
     * 发送数据
     * @param {string} data 数据
     * @returns {boolean} 是否成功发送
     */
    sendString(data) {
        if (this.ConnectionModel === null || this.ConnectionModel === undefined)
            return false;

        if (this.ConnectionModel.status != ConnectionStatus.Connected)
            return false;

        if (this.ConnectionModel.HOST_OBJECT === null || this.ConnectionModel.HOST_OBJECT === undefined)
            return false;

        if (this.ConnectionModel.HOST_OBJECT.Source === null || this.ConnectionModel.HOST_OBJECT.Source === undefined)
            return false;

        this.ConnectionModel.HOST_OBJECT.Source.SendString(data);

        return true;
    }
}

export { UdpConnectionSource }