import { DeviceSourceModelWrapperBase } from './index'

/**
 * Ping源模型包装器
 */
class PingSourceModelWrapper extends DeviceSourceModelWrapperBase {

    /**
     * Ping连接源
     * @param {any} HOST_OBJECT 设备源宿主对象
     */
    constructor(HOST_OBJECT) {
        super(HOST_OBJECT);
    }
}

export { PingSourceModelWrapper }