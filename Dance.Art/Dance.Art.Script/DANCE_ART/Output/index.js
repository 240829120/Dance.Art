import { ServerWrapper } from '../Core/server'

/**
 * 输出服务
 */
class OutputServer {

    constructor() {

        /**
         * 服务包装器
         */
        this.serverWrapper = new ServerWrapper();
    }

    /**
     * 输出行
     * @param {any} msg 消息
     */
    writeLine(msg) {
        let request = { msg : msg }

        this.serverWrapper.invoke("Output/WriteLine", request);
    }
}

export { OutputServer }