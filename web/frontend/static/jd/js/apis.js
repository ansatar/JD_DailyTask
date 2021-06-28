var api = window.api || {};

/**
 * 京东用户基本信息
 */
api.CreateOrUpdateUser = function(data, callback) {
    $.ajax({
        //async: false,
        type: 'POST',
        url: '/api/User/CreateOrUpdateUser',
        contentType: 'application/json;charset=utf-8',
        data: JSON.stringify(data),
        success: function(response) {
            callback(response);
        }
    })
}

/**
 * 获取京东用户自动任务记录
 */
api.GetUserTaskRecords = function(pageIndex, pageSize, callback) {
    $.ajax({
        //async: false,
        type: 'GET',
        url: '/api/User/GetUserTaskRecords',
        data: { pt_pin: localStorage.JD_Api_Access_Pt_Pin, pageIndex: pageIndex, pageSize: pageSize },
        success: function(response) {
            callback(response);
        }
    })
}