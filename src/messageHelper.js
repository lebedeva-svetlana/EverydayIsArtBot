'use strict';

const { HtmlHelper } = require('./htmlHelper');
const { selectors } = require("./selectors");

module.exports.getDescription = function ($) {
    let descriptionSelector = selectors['description'];
    if (!HtmlHelper.isNodeExist($, descriptionSelector)) {
        return '';
    }

    let descriptionArea = HtmlHelper.getElementNodes($, descriptionSelector, '> div').map(elem => elem.replace('\n', '').trim());
    let text = '\n\nОписание:\n\n' + descriptionArea.join('\n\n');

    return text;
}

module.exports.getMain = function ($, url) {
    let text = '';

    let nameArea = HtmlHelper.getTextNodes($, selectors['name']);
    text += '\nНазвание: ' + nameArea[0];
    if (nameArea[1] !== undefined) {
        text += '\nДата создания: ' + nameArea[1];
    }

    let authorSelector = selectors['author'];
    if (HtmlHelper.isNodeExist($, authorSelector)) {
        let authorArea = HtmlHelper.getTextNodes($, authorSelector);
        text += '\n\nАвтор: ' + authorArea[0];
        if (authorArea[1] !== undefined) {
            text += ' ' + authorArea[1];
        }
    }

    let mediumSelector = selectors['medium'];
    if (HtmlHelper.isNodeExist($, mediumSelector)) {
        let mediumArea = HtmlHelper.getElementNodes($, mediumSelector, '> div');
        mediumArea = mediumArea.map(elem => removeExtraSpaces(elem.replace('\n', ' ').replace(' - ', ': ')));

        text += '\n';
        for (let i = 0; i < mediumArea.length; ++i) {
            text += '\n' + mediumArea[i];
            if (i == 2) {
                text += '\n';
            }
        }
    }

    text += `\n\n<a href="${url}">Из собрания Третьяковской галереи</a>`;
    return text;
}

function removeExtraSpaces(text) {
    return text.replace(/\s{2,}/g, ' ').trim();
}