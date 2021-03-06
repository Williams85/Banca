﻿
function ValParametros(metodo, parametros) {
    if (metodo == "POST" || metodo == "PUT" || metodo == "DELETE") {
        return JSON.stringify(parametros)
    }
    else if (metodo == "GET") {
        return parametros
    }
}

function Error(err, status) {
    console.log(err);
    console.log(status);
}

function ajax(info, successFunc, errorFunc) {
    $.ajax({
        type: info.metodo,
        url: info.serviceURL,
        data: ValParametros(info.metodo, info.parametros),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: Error
    });
}

function ajaxNull(info, successFunc, errorFunc) {
    $.ajax({
        type: info.metodo,
        url: info.serviceURL,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: Error
    });
}

function ajaxPartialViewNull(info, succesFunc) {
    $.ajax({
        url: info.serviceURL,
        contentType: 'application/json; charset=utf-8',
        type: info.metodo,
        dataType: 'html'
    })
    .success(succesFunc)
    .error(function (xhr, status, data) {
        console.log(data);
        console.log(status);
    })
}

function ajaxPartialView(info) {
    $.ajax({
        url: info.serviceURL,
        contentType: 'application/json; charset=utf-8',
        type: info.metodo,
        data: ValParametros(info.metodo, info.parametros),
        dataType: 'html',
        success: successFunc,
        error: Error
    });
    //.success(function (result) {
    //    if (result != null) {
    //        $(info.objcontenedor).html(result);
    //    }
    //})
    //.error(function (xhr, status, data) {
    //    console.log(data);
    //    console.log(status);
    //})
}

function ajaxPartialView(info, successFunc) {
    $.ajax({
        url: info.serviceURL,
        contentType: 'application/json; charset=utf-8',
        type: info.metodo,
        data: ValParametros(info.metodo, info.parametros),
        dataType: 'html',
        success: successFunc,
        error: Error
    })
    //.success(successFunc)
    //.error(function (xhr, status, data) {
    //    console.log(data);
    //    console.log(status);
    //})
}
