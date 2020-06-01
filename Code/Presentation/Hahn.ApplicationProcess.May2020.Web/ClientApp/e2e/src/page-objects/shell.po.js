"use strict";
/*
 * Use the Page Object pattern to define the page under test.
 * See docs/coding-guide/e2e-tests.md for more info.
 */
Object.defineProperty(exports, "__esModule", { value: true });
var protractor_1 = require("protractor");
var ShellPage = /** @class */ (function () {
    function ShellPage() {
        this.welcomeText = protractor_1.element(protractor_1.by.css('app-root h1'));
    }
    ShellPage.prototype.getParagraphText = function () {
        return this.welcomeText.getText();
    };
    return ShellPage;
}());
exports.ShellPage = ShellPage;
//# sourceMappingURL=shell.po.js.map