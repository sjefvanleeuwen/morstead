/**
 * @class Notification
 * Listens for click events on the close button for notification to close the notification using javascript
 */
var Notification = (function () {
    function Notification(element) {
        this.element = element;
    }
    /**
     * Closes the notification
     */
    Notification.prototype.close = function () {
        this.element.parentNode.removeChild(this.element);
    };
    return Notification;
})();
exports.Notification = Notification;
//# sourceMappingURL=notification.js.map