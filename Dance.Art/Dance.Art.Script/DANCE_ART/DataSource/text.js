import { DataSourceModelWrapperBase } from './index'

/**
 * 文本数据模型包装器
 */
class TextDataSourceModelWrapper extends DataSourceModelWrapperBase {

    /**
     * 文本数据模型包装器
     * @param {any} HOST_OBJECT 设备源宿主对象
     */
    constructor(HOST_OBJECT) {
        super(HOST_OBJECT);
    }

    /**
     * 获取文本
     * @returns {string}
     */
    get text() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Text;
    }
}

export { TextDataSourceModelWrapper }