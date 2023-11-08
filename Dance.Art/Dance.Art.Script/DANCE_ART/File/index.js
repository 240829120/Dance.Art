/**
 * 文件服务包装
 */
class FileScriptServiceWrapper {

    /**
     * 文件服务
     */
    constructor() {

        /**
         * 服务命名空间
         */
        this.NAME_SPACE = "DANCE_ART_SCRIPT";

        /**
         * 服务名称
         */
        this.NAME = "FileScriptService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 读取文本文件
     * @param {string} path 文件路径
     */
    readTxtFile(path) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.ReadTxtFile(path);
    }

    /**
     * 写入文本文件
     * @param {string} path 文件路径
     * @param {string} content 内容
     */
    writeTxtFile(path, content) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return;

        this.HOST_OBJECT.WriteTxtFile(path, content);
    }
}

export { FileScriptServiceWrapper }