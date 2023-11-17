import { DocumentWrapperCollectionWrapper } from '../Document/index'
import { ControlGridItemModelBaseWrapper } from './index'


/**
 * 下拉选框包装器
 */
class ComboBoxModelWrapper extends ControlGridItemModelBaseWrapper {

    /**
     * 下拉选框包装器
     * @param {any} host 宿主对象
     */
    constructor(host) {
        super(host);
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* value */

    /**
     * 获取值
     * @returns {string}
     */
    get value() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Value;
    }

    /**
     * 设置值
     * @returns {string}
     */
    set value(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Value = value;
    }

    /* ----------------------------------------------------- */
    /* items */

    /**
     * 项集合
     * @returns {DocumentWrapperCollectionWrapper}
     */
    get items() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return new DocumentWrapperCollectionWrapper(this.HOST_OBJECT.Items);
    }
}

export { ComboBoxModelWrapper }