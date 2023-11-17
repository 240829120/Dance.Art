import { DocumentItemModelWrapper } from '../Document/index'

/**
 * 资源项模型包装器
 */
class ResourceItemModelWrapper extends DocumentItemModelWrapper {

    /**
     * 资源项模型包装器
     * @param {any} host 宿主对象
     */
    constructor(host) {
        super(host);

    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* id */

    /**
     * 获取编号
     * @returns {string}
     */
    get id() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.ID;
    }
}

/**
 * 资源元素模型包装器
 */
class ResourceElementModelWrapper extends ResourceItemModelWrapper {

    /**
     * 资源项模型包装器
     * @param {any} host 宿主对象
     */
    constructor(host) {
        super(host);
    }
}

export { ResourceItemModelWrapper, ResourceElementModelWrapper }