$(function () {
    $("#htmEditor").ckeditor();
    var msgBodyHeight = $(window).height() - $("#header").height();
    
    $("#btnDockBody").click(function () {
        $("#postPage").hide("slow");
        $("#header").hide("slow");
        $("#MsgDiv").appendTo("#postPageAlt");

        //$("#Body").data("tEditor").height(msgBodyHeight);
        $("#postPageAlt").height(msgBodyHeight).show("slow");

        
        
    });
})