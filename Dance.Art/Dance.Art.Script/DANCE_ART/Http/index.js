/**
 * Http服务包装
 */
class HttpScriptServiceWrapper {

    /**
     * Http服务
     */
    constructor() {

        /**
         * 服务命名空间
         */
        this.NAME_SPACE = "DANCE_ART_SCRIPT";

        /**
         * 服务名称
         */
        this.NAME = "HttpScriptService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * Post请求
     * @param {string} url 地址
     * @param {string} data 数据
     */
    post(url, data) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Post(url, data);
    }

    /**
     * Get请求
     * @param {string} url 地址
     */
    get(url) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Get(url);
    }
}

export { HttpScriptServiceWrapper }