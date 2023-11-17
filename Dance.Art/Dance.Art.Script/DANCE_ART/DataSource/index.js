
/* ===================================================================================== */
/* 数据集 */

/**
 * 数据集单元格包装器
 */
class DataSetCellModelWrapper {
    /**
     * 数据集单元格包装器
     * @param {any} HOST_OBJECT 宿主对象
     */
    constructor(HOST_OBJECT) {
        this.HOST_OBJECT = HOST_OBJECT;
    }

    /**
     * 获取行号
     * @returns {number}
     */
    get row() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Row;
    }

    /**
     * 获取列号
     * @returns {number}
     */
    get column() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Column;
    }

    /**
     * 获取值
     * @returns {string}
     */
    get value() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Value;
    }
}


/**
 * 数据集包装器
 */
class DataSetModelWrapper {
    /**
     * 数据集包装器
     * @param {any} HOST_OBJECT 宿主对象
     */
    constructor(HOST_OBJECT) {
        this.HOST_OBJECT = HOST_OBJECT;
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* content */

    /**
     * 获取最小行号
     * @returns {number}
     */
    get minRow() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.MinRow;
    }

    /**
     * 获取最大行号
     * @returns {number}
     */
    get maxRow() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.MaxRow;
    }

    /**
     * 获取最小列号
     * @returns {number}
     */
    get minColumn() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.MinColumn;
    }

    /**
     * 获取最小列号
     * @returns {number}
     */
    get maxColumn() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.MaxColumn;
    }

    /* ============================================================================================ */
    /* Function */

    /**
     * 获取单元格
     * @param {number} row 行号
     * @param {number} column 列号
     * @returns {DataSetCellModelWrapper}
     */
    getCell(row, column) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        let host = this.HOST_OBJECT.GetCell(row, column);
        if (host === null || host === undefined)
            return null;

        let result = new DataSetCellModelWrapper(host);
        return result;
    }
}

/* ===================================================================================== */
/* 服务 */

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

export { DataSetCellModelWrapper, DataSetModelWrapper, DataSourceModelWrapperBase, DataSourceScriptServiceWrapper }