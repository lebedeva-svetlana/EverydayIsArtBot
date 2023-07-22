'use strict';

const { selectors } = require('./selectors');
const { HtmlHelper } = require("./htmlHelper");
const { getRandomInt } = require("./random");

class Tretyakov {
    static getSearchUrl() {
        let url = 'https://my.tretyakov.ru/app/gallery?pageNum=';
        let pageNumb = getRandomInt(1, 389);
        return url + pageNumb;
    }

    static getObjectUrl($) {
        let imageCardNumb = getRandomInt(1, 20);
        let imageLinkSelector = selectors['imageA'].replace('{number}', imageCardNumb);
        let href = 'https://my.tretyakov.ru' + HtmlHelper.getProperty($, imageLinkSelector, 'href');
        return href;
    }
}

module.exports.Tretyakov = Tretyakov;