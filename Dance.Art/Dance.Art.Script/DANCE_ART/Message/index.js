
/**
 * 消息框图标
 */
const MESSAGE_BOX_ICON = {

    /**
     * 空
     */
    None: "None",

    /**
     * 失败
     */
    Failure: "Failure",

    /**
     * 成功
     */
    Success: "Success",

    /**
     * 警告
     */
    Warning: "Warning",

    /**
     * 信息
     */
    Info: "Info"
};

/**
 * 消息框行为
 */
const MESSAGE_BOX_ACTION = {

    /**
     * 确定
     */
    YES: "YES",

    /**
     * 否定
     */
    NO: "NO",

    /**
     * 取消
     */
    CANCEL: "CANCEL"
}

/**
 * 通知图标
 */
const NOTIFY_ICON = {

    /**
     * 空
     */
    None: "None",

    /**
     * 信息
     */
    Info: "Info",

    /**
     * 警告
     */
    Warning: "Warning",

    /**
     * 错误
     */
    _Error: "Error"
}

/**
 * 消息服务
 */
class MessageService {

    /**
     * 消息服务
     */
    constructor() {

        /**
         * 服务命名空间
         */
        this.NAME_SPACE = "DANCE_ART_SCRIPT";

        /**
         * 服务名称
         */
        this.NAME = "MessageService";

        /**
         * 服务宿主对象
         */
        this.HOST_OBJECT = DANCE_ART_HOST.GetService(this.NAME_SPACE, this.NAME);
    }

    /**
     * 显示消息框
     * @param {string} header 标题
     * @param {MESSAGE_BOX_ICON} icon 图标
     * @param {string} content 内容
     * @param {MESSAGE_BOX_ACTION} action 行为
     * @returns {MESSAGE_BOX_ACTION} 点击按钮
     */
    showMessageBox(header, icon, content, action) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            retun;

        let result = this.HOST_OBJECT.ShowMessageBox(`${header}`, `${icon}`, `${content}`, `${action}`);

        return result;
    }

    /**
     * 显示通知
     * @param {string} header 标题
     * @param {NOTIFY_ICON} icon 图标
     * @param {string} content 内容
     */
    showNotify(header, icon, content) {
        if (this.HOST_OBJECT === null || this.HOST_OBJECT === undefined)
            retun;

        this.HOST_OBJECT.ShowNotify(`${header}`, `${icon}`, `${content}`);
    }
}

export { MESSAGE_BOX_ICON, MESSAGE_BOX_ACTION, NOTIFY_ICON, MessageService }