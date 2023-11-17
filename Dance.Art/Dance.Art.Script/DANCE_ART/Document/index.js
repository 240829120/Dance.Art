
/**
 * 文档项列表包装器
 */
class DocumentWrapperCollectionWrapper {

    /**
     * 文档项列表包装器
     * @param {any} host 宿主对象
     */
    constructor(host) {

        /**
         * 宿主对象
         */
        this.HOST_OBJECT = host;
    }

    /**
     * 添加项
     * @param {any} item 项
     */
    add(item) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        this.HOST_OBJECT.Add(item);
    }

    /**
     * 移除项
     * @param {any} item 项
     */
    remove(item) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        this.HOST_OBJECT.Remove(item);
    }

    /**
     * 移除项
     * @param {number} index 项索引
     */
    removeAt(index) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        this.HOST_OBJECT.RemoveAt(item);
    }

    /**
     * 清理
     */
    clear() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        this.HOST_OBJECT.Clear();
    }
}


/**
 * 文档项模型包装器
 */
class DocumentItemModelWrapper {

    /**
     * 文档项模型包装器
     * @param {any} host 宿主对象
     */
    constructor(host) {

        /**
         * 宿主对象
         */
        this.HOST_OBJECT = host;
    }

    /* ============================================================================================ */
    /* Property */

    /* ----------------------------------------------------- */
    /* ownerDocumentHost */

    /**
     * 获取所属文档
     * @returns {any}
     */
    get ownerDocumentHost() {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            return null;

        return this.HOST_OBJECT.OwnerDocument;
    }

}


export { DocumentWrapperCollectionWrapper, DocumentItemModelWrapper }