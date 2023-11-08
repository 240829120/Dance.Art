import { DeviceReceiveBufferDataEventArgsWrapper, DeviceSourceModelWrapperBase } from './index'

/**
 * UDP源包装器
 */
class UdpSourceModelWrapper extends DeviceSourceModelWrapperBase {

    /**
     * UDP连接源
     * @param {ConnectionModel} model 连接模型
     */
    constructor(HOST_OBJECT) {
        super(HOST_OBJECT);
    }

    /**
     * 接收数据时触发
     * @param {Function} func func(any, DeviceReceiveBufferDataEventArgsWrapper)
     * @returns {boolean} 是否连接成功
     */
    onReceiveData(func) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return false;

        this.HOST_OBJECT.ReceiveData.connect((s, e) => {
            let args = new DeviceReceiveBufferDataEventArgsWrapper(e);
            func(s, args);
        });

        return true;
    }

    /**
     * 发送数据
     * @param {Int8Array} buffer 二进制数据
     * @returns {number} 发送数据长度
     */
    send(buffer) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return 0;

        let result = this.HOST_OBJECT.Send(data.buffer);

        return result;
    }

    /**
     * 发送数据
     * @param {string} str 字符串
     * @returns {boolean} 是否成功发送
     */
    sendString(str) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return 0;

        let result = this.HOST_OBJECT.SendString(str);

        return result;
    }
}

export { UdpSourceModelWrapper }