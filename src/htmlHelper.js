'use strict';

const cheerio = require('cheerio');

class HtmlHelper {
    static async getHtml(url) {
        let response = await fetch(url);
        if (response.ok) {
            return await response.text();
        }
        else {
            throw new Error(`Во время запроса возникло исключение. Код статуса: ${response.status}`);
        }
    }

    static getProperty($, selector, property) {
        return $(selector).prop(property);
    }

    static getSrc(html, selector) {
        const $ = cheerio.load(html);
        $.html();
        return this.getProperty($, selector, 'src');
    }

    static getElementNodes($, parentSelector, childSelector) {
        return [...$(parentSelector)].map(e => [...$(e).find(childSelector)].map(e => this.getProperty($, e, 'innerText')))[0];
    }

    static getTextNodes($, parentSelector) {
        let text = this.getProperty($, parentSelector, 'innerText');
        text = text.split('\n');
        text = text.map(elem => elem.trim()).filter(elem => elem);
        return text;
    }

    static isNodeExist($, nodeSelector) {
        return $(nodeSelector).length !== 0;
    }
}

module.exports.HtmlHelper = HtmlHelper;