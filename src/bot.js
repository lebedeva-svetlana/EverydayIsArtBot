'use strict';

const { Telegraf } = require('telegraf');
const cheerio = require('cheerio');
require('dotenv').config();

const { HtmlHelper } = require('./htmlHelper');
const { Tretyakov } = require('./tretyakov');
const { selectors } = require("./selectors");
const { getDescription, getMain } = require("./messageHelper");

let BOT_TOKEN = '';
const bot = new Telegraf(BOT_TOKEN);

bot.catch((err) => {
    console.log(err);
});

bot.start(async (ctx) => {
    await ctx.replyWithHTML('\n\nEverydayIsArt отправляет случайный экспонат из собрания <a href="https://www.tretyakovgallery.ru/?lang=ru">Государственной Третьяковской галереи</a>.\n\nОтправьте команду /get для получения экспоната.');
});

bot.command('get', async (ctx) => {
    let searchUrl = Tretyakov.getSearchUrl();
    let searchHtml = await HtmlHelper.getHtml(searchUrl);

    let $ = cheerio.load(searchHtml);
    $.html();

    let objectUrl = Tretyakov.getObjectUrl($);
    let objectHtml = await HtmlHelper.getHtml(objectUrl);

    let imageSource = HtmlHelper.getSrc(objectHtml, selectors['image']);

    $ = cheerio.load(objectHtml);
    $.html();

    let main = getMain($, objectUrl);
    let description = getDescription($);

    await ctx.replyWithPhoto(imageSource, { caption: main, parse_mode: 'HTML' });
    await ctx.reply(description);
});

bot.launch();

process.once('SIGINT', () => bot.stop('SIGINT'));
process.once('SIGTERM', () => bot.stop('SIGTERM'));