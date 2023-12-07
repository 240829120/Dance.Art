/**
 * 服务返回码
 */
let ServerResponseCode = {
    SUCCESS: "SUCCESS"
}

/**
 * 服务返回
 */
class ServerResponse {
    constructor() {

        /**
         * 返回码
         */
        this.code = "";

        /**
         * 消息
         */
        this.msg = "";
    }
}

/**
 * 服务包装器
 */
class ServerWrapper {

    /**
     * 调用
     * @param {string} route 路由
     * @param {...string} args 参数
     */
    invoke(route, ...args) {
        let array = new Array();
        for (var i = 0; i < args.length; i++) {
            array.push(JSON.stringify(args[i]));
        }

        let resultStr = DANCE_ART_HOST.Invoke(route, array);
        let result = JSON.parse(resultStr);

        return result;
    }
}

export { ServerResponseCode, ServerResponse, ServerWrapper }