
function Indicator(indicatorImgSrc) {
    var indicatorHtml = '<div id="indicatorBar" class="indicatorBarStyle">' + '<label id="indicatorText"></label> <img src="/Content/Images/ajax-loader.gif"/> ' + '</div>';

    this.show = function (indicatorMsg) {
        //        if (typeof updatingElemLoc == 'undefined') {
        //            updatingElemLoc = 'center';
        //        }
        //        if (typeof indicatorLoc == 'undefined') {
        //            indicatorLoc = 'center';
        //        }
        //        
        //        $(indicatorHtml).appendTo('body');
        //        $('#indicatorImg').position({
        //            my: indicatorLoc,
        //            at: updatingElemLoc,
        //            of: '#' + updatingElemId
        //        }).show('fast');
        
        $(indicatorHtml).appendTo('body');
        $('#indicatorText').html(indicatorMsg);
    };

    this.hide = function () {
        $('#indicatorBar').remove();
    };
}