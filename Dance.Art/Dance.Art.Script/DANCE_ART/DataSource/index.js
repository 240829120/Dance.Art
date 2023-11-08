/**
 * 数据模型包装器基类
 */
class DataSourceModelWrapperBase {

    /**
     * 设备源模型包装器基类
     * @param {any} HOST_OBJECT 宿主对象
     */
    constructor(HOST_OBJECT) {

        /**
         * 宿主对象
         */
        this.HOST_OBJECT = HOST_OBJECT;
    }
}

/**
 * 数据脚本服务包装器
 */
class DataSourceScriptServiceWrapper {

    /**
     * 数据脚本服务包装器
     */
    constructor() {

        /**
         * 服务命名空间
         */
        this.NAME_SPACE = "DANCE_ART_SCRIPT";

        /**
         * 服务名称
         */
        this.NAME = "DataSourceScriptService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 获取数据源
     * @param {string} name 名称
     * @returns {any} 设备源 HOST_OBJECT
     */
    getDataSource(name) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            retun;

        let result = this.HOST_OBJECT.GetDataSource(`${name}`);

        return result;
    }
}

export { DataSourceModelWrapperBase, DataSourceScriptServiceWrapper }