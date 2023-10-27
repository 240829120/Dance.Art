
/**
 * 输出服务
 */
class OutputService {

    /**
     * 输出服务
     */
    constructor() {

        /**
         * 服务命名空间
         */
        this.NAME_SPACE = "DANCE_ART_SCRIPT";

        /**
         * 服务名称
         */
        this.NAME = "OutputService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 输出日志
     * @param {string} txt 日志
     */
    log(txt) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            retun;

        this.HOST_OBJECT.Log(`${txt}`);
        console.log(`${txt}`);
    }
}

export { OutputService }