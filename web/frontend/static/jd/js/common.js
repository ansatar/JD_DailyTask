/// ref: momentjs

var timeFormat = 'YYYY/MM/DD HH:mm:ss';
var shortTimeFormat = 'YYYY/MM/DD HH:mm';

/**
 * 是否已登录
 */
function isLogined() {
    if (!localStorage.JD_Api_Access_Cookies) return false;
    if (moment() > moment(localStorage.JD_Api_Access_Cookies_Expires, timeFormat)) return false;

    return true;
}

/**
 * 本地登录
 * cookies: cookies
 * expires: 过期时间（天），默认为一个月
 */
function saveLoginStorage(cookies, expires) {
    localStorage.JD_Api_Access_Cookies = cookies;
    localStorage.JD_Api_Access_Cookies_LoginTime = moment().format(shortTimeFormat);
    if (expires)
        localStorage.JD_Api_Access_Cookies_Expires = moment().add(expires, 'd').format(timeFormat);
    else
        localStorage.JD_Api_Access_Cookies_Expires = moment().add(1, 'month').format(timeFormat);
}

/**
 * 移除登录状态
 */
function removeLoginStorage() {
    localStorage.removeItem('JD_Api_Access_Cookies');
    localStorage.removeItem('JD_Api_Access_Cookies_Expires');
    localStorage.removeItem('JD_Api_Access_Cookies_LoginTime');
}

function savePtPinStorage(pt_pin) {
    localStorage.JD_Api_Access_Pt_Pin = pt_pin;
}