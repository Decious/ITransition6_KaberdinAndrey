const connection = new signalR.HubConnectionBuilder().withUrl("/indexhub").build();


connection.start();
connection.on("addSticker", addSticker);
connection.on("setStickerColor", setStickerColor);
connection.on("removeSticker", removeSticker);
connection.on("setStickerPosition", setStickerPosition);
connection.on("setStickerSize", setStickerSize);
connection.on("setStickerContent", setStickerContent);

function addSticker(id, x, y, width, height, color, content) {
    $("#WorkPlace").append(defaultStickerHTML);
    let newSticker = $("#NewSticker");
    if (x != null && y != null) setStickerPosition(newSticker, x, y);
    if (width != null && height != null) setStickerSize(newSticker, width, height);
    if (color != null) setStickerColor(newSticker, color);
    if (content != null) setStickerContent(newSticker, content);
    newSticker.attr("id", id);
    initSticker(newSticker[0]);
}
function setStickerColor(sticker, color) {
    sticker = getStickerElement(sticker);
    let title = $(sticker).children("[name='title']");
    let content = $(sticker).children("[name='content']");
    switch (color) {
        case 0:
            $(title).css("background-color", "#00BBF9");
            $(content).css("background-color", "highlight");
            break;
        case 1:
            $(title).css("background-color", "#F15BB5");
            $(content).css("background-color", "pink");
            break;
        case 2:
            $(title).css("background-color", "#DEC402");
            $(content).css("background-color", "#FDE74C");
            break;
    }
}
function removeSticker(id) {
    $("#"+id).remove();
}
function setStickerPosition(sticker, x, y) {
    sticker = getStickerElement(sticker);
    sticker.attr("data-x", x);
    sticker.attr("data-y", y);
    sticker.css({ "transform": "translate(" + x + "px, " + y + "px)" });
}
function setStickerSize(sticker, width, height) {
    sticker = getStickerElement(sticker);
    sticker.css("width", width);
    sticker.css("height", height);
}
function setStickerContent(sticker, content) {
    sticker = getStickerElement(sticker);
    $(sticker).children("[name='content']").html(content);
}

const defaultStickerHTML = "<div name='newSticker' id='NewSticker' class='draggable resizable position-absolute sticker'>" +
    "<div name = 'title' style = 'min-height:24px; min-width:150px; background-color: #DEC402'>" +
    "<div class='row'>" +
    "<div class='col-10'>" +
    "<button class='border-0 bg-transparent' style='color:highlight' value='highlight'><i class='fas fa-square'></i></button>" +
    "<button class='border-0 bg-transparent' style='color:pink' value='pink'><i class='fas fa-square'></i></button>" +
    "<button class='border-0 bg-transparent' style='color:yellow' value='yellow'><i class='fas fa-square'></i></button>" +
    "</div>" +
    "<div class='col-2 justify-content-end d-flex'>" +
    "<button class='border-0 bg-transparent' style='color:snow' value='close'><i class='fa fa-window-close'></i></button>" +
    "</div>" +
    "<button class='border-0 bg-transparent' style='color:crimson; position:absolute; right:0px; top:24px;' value='edit'><i class='fa fa-edit'></i></button>" +
    "</div>" +
    "</div>" +
    "<div name='content' class='text-wrap text-break text-truncate' style='height:100%; background-color:#FDE74C'>" +
    "</div>" +
    "</div>";

$('#WorkPlace').on('click', function (e) {
    if (e.target !== this)
        return;
    requestAddSticker();
});







function requestAddSticker() {
    connection.invoke("AddDefaultSticker");
}
function initSticker(element) {
    $(element).attr("name", "sticker");
    makeStickerDraggable(element);
    makeStickerResizable(element);
    hookUpStickerEvents(element);
    hideControls(element);
}
function makeStickerDraggable(element) {
    interact(element).draggable({
        modifiers: [
            interact.modifiers.restrictRect({
                restriction: 'parent',
                endOnly: false
            })
        ],
        inertia: true,
        onmove: dragMoveListener,
        onend: dragMoveEnd
    });
}
function makeStickerResizable(element) {
    interact(element).resizable({
        edges: {
            top: false,
            left: true,
            bottom: true,
            right: true
        },
        modifiers: [
            interact.modifiers.restrictRect({
                restriction: 'parent',
                endOnly: false
            })
        ],
        onstart: blockHover,
        onmove: resizeListener,
        onend: resizeEndHandler
    });
}
function blockHover(event) {
    let sticker = getSticker($(event.target));
    $(sticker).unbind('mouseenter mouseleave')
}
function resizeEndHandler(event) {
    let sticker = getSticker($(event.target));
    $(sticker).hover(hoverListener, mouseLeaveListener);
    let width = parseInt($(sticker).css("width"));
    let height = parseInt($(sticker).css("height"));
    let stickerID = $(sticker).attr('id');
    let x = parseInt(sticker.attr('data-x'));
    let y = parseInt(sticker.attr('data-y'));
    connection.invoke("ResizeSticker", +stickerID, width, height,x,y);
}
function hookUpStickerEvents(element) {
    $(element).hover(hoverListener, mouseLeaveListener);
    $(element).click(focus);
    let buttons = $(element).find("button");
    $(buttons).each(function (index) {
        switch (this.value) {
            case "close":
                $(this).on('click', requestStickerRemove);
                break;
            case "edit":
                $(this).on('click', editSticker);
                break;
            case "acceptEdit":
                $(this).on('click', acceptEditSticker);
                break;
            default:
                $(this).on('click', requestStickerColorEdit);
                break;
        }
    })
}
function hideControls(element) {
    let titleElement = getTitleElement(element);
    $(titleElement).find("button").hide();
}
function focus(event) {
    let stickers = $("[name='sticker']");
    let eventSticker;
    eventSticker = getSticker(event.target);
    $(stickers).css("z-index", 0);
    $(eventSticker).css("z-index", 1);
}
function resizeListener(event) {
    let { x, y } = event.target.dataset

    x = (parseFloat(x) || 0) + event.deltaRect.left
    y = (parseFloat(y) || 0) + event.deltaRect.top

    Object.assign(event.target.style, {
        width: `${event.rect.width}px`,
        height: `${event.rect.height}px`,
        transform: `translate(${x}px, ${y}px)`
    })

    Object.assign(event.target.dataset, { x, y })
}

function dragMoveListener(event) {
    let target = event.target;
    x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
    y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;

    target.style.webkitTransform = target.style.transform
        = 'translate(' + x + 'px, ' + y + 'px)';
    target.setAttribute('data-x', x);
    target.setAttribute('data-y', y);
}

function dragMoveEnd(event) {
    let target = event.target;
    let x = parseInt(target.getAttribute('data-x'));
    let y = parseInt(target.getAttribute('data-y'));
    let id = target.getAttribute('id');
    connection.invoke("MoveSticker", +id, +x, +y).catch(function (err) {
        return console.error(err.toString());
    });
}

function hoverListener(event) {
    let element = $(event.target);
    let titleElement = getTitleElement(element);
    $(titleElement).find("button").show(250);
}
function mouseLeaveListener(event) {
    let element = $(event.target);
    let titleElement = getTitleElement(element);
    $(titleElement).find("button").hide(250);
}
function requestStickerColorEdit(event) {
    let buttonElement = getButtonElement($(event.target));
    let sticker = getSticker(buttonElement);
    let stickerID = +$(sticker).attr("id");
    switch ($(buttonElement).val()) {
        case "highlight":
            connection.invoke("EditStickerColor", stickerID, 0);
            break;
        case "pink":
            connection.invoke("EditStickerColor", stickerID, 1);
            break;
        case "yellow":
            connection.invoke("EditStickerColor", stickerID, 2);
            break;
    }
}
function requestStickerRemove(event) {
    let buttonElement = getButtonElement($(event.target));
    let sticker = getSticker(buttonElement);
    connection.invoke("RemoveSticker", +$(sticker).attr("id"));
}
function editSticker(event) {
    let turndownService = new TurndownService();
    let buttonElement = getButtonElement($(event.target));
    let sticker = $(buttonElement).closest("[name='sticker']");
    let content = $(sticker).children("[name='content']");
    content.html("<textarea style='width: 100%; height: 100%; background: transparent;border: none;' >" + turndownService.turndown(content.html())+"</textarea>");
    $(content).children("textarea").focus();
    $(buttonElement).unbind('click');
    $(buttonElement).on('click', acceptEditSticker);
}
function acceptEditSticker(event) {
    let markdown = window.markdownit();
    let buttonElement = getButtonElement($(event.target));
    let sticker = $(buttonElement).closest("[name='sticker']");
    let content = $(sticker).children("[name='content']");
    let markdownText = $(content).children("textarea").val();
    let html = markdown.render(markdownText);
    let stickerID = $(sticker).attr('id');
    $(buttonElement).unbind('click');
    $(buttonElement).on('click', editSticker);
    connection.invoke("EditContentSticker", +stickerID, html);
}
function getButtonElement(target) {
    if ($(target).is("button")) {
        return buttonElement = $(event.target);
    }
    else {
        return buttonElement = $(event.target).parents("button");
    }
}
function getSticker(target) {
    if ($(target).is("[name='sticker']")) {
        sticker = $(target);
    } else {
        sticker = $(target).closest("[name='sticker']");
    }
    return sticker;
}
function getTitleElement(element) {
    if ($(element).is("[name='sticker']"))
        return $(element).children("[name='title']");
    else
        return $(element).siblings("[name='title']");
}
function getStickerElement(sticker) {
    if (!isNaN(sticker)) {
        sticker = $("#" + sticker);
    }
    return sticker;
}
