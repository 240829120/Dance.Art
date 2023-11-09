import { DataSetModelWrapper, DataSourceModelWrapperBase } from './index'



/**
 * Excel数据模型包装器
 */
class ExcelDataSourceModelWrapper extends DataSourceModelWrapperBase {

    /**
     * 文本数据模型包装器
     * @param {any} HOST_OBJECT 设备源宿主对象
     */
    constructor(HOST_OBJECT) {
        super(HOST_OBJECT);
    }

    /**
     * 获取数据集
     * @param {string} name 数据集名称
     * @returns {DataSetModelWrapper} 数据集
     */
    getDataSet(name) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        let host = this.HOST_OBJECT.GetDataSet(name);
        if (host === null || host === undefined)
            return null;

        let result = new DataSetModelWrapper(host);
        return result;
    }
}

export { ExcelDataSourceModelWrapper }