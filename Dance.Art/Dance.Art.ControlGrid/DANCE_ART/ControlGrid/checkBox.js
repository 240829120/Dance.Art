import { ControlGridItemModelBaseWrapper } from './index'

/**
 * 复选框包装器
 */
class CheckBoxModelWrapper extends ControlGridItemModelBaseWrapper {

    /**
     * 复选框包装器
     * @param {any} host 宿主对象
     */
    constructor(host) {
        super(host);
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* content */

    /**
     * 获取内容
     * @returns {string}
     */
    get content() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Content;
    }

    /**
     * 设置行
     * @returns {string}
     */
    set content(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.Content = value;
    }

    /* ----------------------------------------------------- */
    /* IsChecked */

    /**
     * 获取是否选中
     * @returns {boolean}
     */
    get isChecked() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.IsChecked;
    }

    /**
     * 设置是否选中
     * @returns {boolean}
     */
    set isChecked(value) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.IsChecked = value;
    }
}

export { CheckBoxModelWrapper }