var api = window.api || {};

/**
 * 获取相关的登录参数
 */
api.GetLoginParams = function(callback) {
    $.ajax({
        //async: false,
        type: 'GET',
        url: '/api/UserVerif/GetLoginParams',
        success: function(response) {
            callback(response);
        }
    })
}

/**
 * 检查是否登录成功
 */
api.CheckLogin = function(cookies, token, okl_token, callback) {
    $.ajax({
        //async: false,
        type: 'POST',
        url: '/api/UserVerif/CheckLogin',
        contentType: 'application/json;charset=utf-8',
        data: JSON.stringify({ cookies: cookies, token: token, okl_token: okl_token }),
        success: function(response) {
            callback(response);
        }
    })
}

/**
 * 京东用户基本信息
 */
api.QueryJDUserInfo = function(cookies, callback) {
    $.ajax({
        //async: false,
        type: 'GET',
        url: '/api/User/QueryJDUserInfo',
        data: { cookies: cookies },
        success: function(response) {
            callback(response);
        }
    })
}

/**
 * 领京豆
 */
api.DailyTask = {}
api.DailyTask.DoAction = function(actionList, callback) {
    var defer = $.Deferred();
    $.ajax({
        // async: false,
        type: 'POST',
        url: '/api/DailyTask/DoAction',
        contentType: 'application/json;charset=utf-8',
        data: JSON.stringify({ pt_pin: localStorage.JD_Api_Access_Pt_Pin, actionList: actionList }),
        success: function(response) {
            //callback(response);
            defer.resolve(response)
        }
    })
    return defer.promise();
}