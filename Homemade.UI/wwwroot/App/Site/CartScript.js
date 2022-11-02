var cartcount = 0;

$(document).ready(function () {
    if (getsessionID() != "" && getsessionID() != undefined) {
        fillCartDataServerSide();
    } else {
        CreateSession();
    }
});
function getTokenCookie() { return cart_getCookie('sessionToken'); }
function getsessionID() { debugger; return cart_getCookie('sessionID'); }

//#region Cart Function
function AddToCart(itemid, quantity, type) {
    debugger;

    if (itemid != undefined) {
        $.ajax({
            type: "GET",
            url: "/site/home/addToCart",
            async: true,
            data: {
                _itemid: itemid,
                _quantity: quantity,
                _sessionID: getsessionID(),
                _type: type
            },

        }).done(function (data) {
            if (data.status) {
                if (data.newsession) {
                    cart_removeCookie('sessionToken');
                    cart_removeCookie('sessionID');
                    //------------------------------------------------
                    cart_setCookie("sessionToken", data.sessionToken, 7);
                    cart_setCookie("sessionID", data.sessionID, 7);
                }
                fillCartBox(
                    data.itemDetailsid,
                    data.itemDetailsguid,
                    data.itemname,
                    data.itemqu,
                    data.itemimg,
                    data.itemprice,
                    data.itemTotal,
                    data.itemID
                );


                //alert('added to cart');
            }
            else {
                $("#divMsgParentAlertMessage").hide();
                $("#divMsgParentAlertMessage").show();
                $("#divMsgClassAlertMessage").attr("class", "alert alert-site");
                $("#lblMsgAlertMessage").html(data.message);
                $("#AlertMessagemodal").modal('show');
                setTimeout(function () {
                    $("#AlertMessagemodal").modal('hide');
                }, 3000);
            }
        });

    }
}
function fillCartBox(itemDetailsid, itemDetailsguid, itemname, itemqu, itemimg, itemprice, itemTotal, itemID) {
    debugger;
    $('.cart-dropdown-visible').removeClass('d-none');
    $('.active-point').removeClass('d-none');
    $('.cart-dropdown-empty').addClass('d-none');

    if ($('.item-' + itemDetailsid).length)
        $('.item-' + itemDetailsid).remove();

    cartcount = cartcount + 1;

    $('.cart-items-list').append(
        `
<li class="list-item item-`+ itemDetailsid + `">
                                            <div class="cart-item">
                                                <div class="item-img">
                                                    <a href="/Site/home/produc_default?id=`+ itemDetailsguid + `">
                                                        <img class="img-fluid" src="https://cdn.made-home.com/Home/Product/`+ itemimg + `">
                                                    </a>
                                                </div>
                                                <div class="item-details">
                                                    <div class="item-wrap item-title-wrap">
                                                        <a class="product-name-width"  href="/Site/home/produc_default?id=`+ itemDetailsguid + `" class="item-title">` + itemname + ` ` + itemqu + `x</a>
                                                        <div class="delete-item"><i class="far fa-trash-alt" onclick="RemoveFromCart(`+ itemID + `)"></i></div>
                                                    </div>
                                                    <div class="item-wrap item-info">
                                                         
                                                        <div class="item-price">
                                                            <span>`+ itemTotal + `</span>
                                                            ريال
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </li>
`
    );
    if (cartcount > 0) {
        $('.active-point-cart').show();
        $('.active-point-cart').html(cartcount);
    }
    else {
        $('.active-point-cart').hide();
    }
}
function CreateSession() {
    var ajaxReq = $.ajax({
        type: "POST",
        url: "/site/home/CreateSession",
        async: true,
        data: {
            _sessionID: getTokenCookie()
        },
    }).done(function (data) {
        debugger;
        cart_setCookie("sessionID", data.sessionID, 7);
        cart_setCookie("sessionToken", data.sessionToken, 7);

    });

}
function fillCartDataServerSide() {
    debugger;
    var ajaxReq = $.ajax({
        type: "POST",
        url: "/site/home/CartData",
        async: true,
        data: {
            _sessionID: getsessionID()
        },
    }).done(function (data) {
        if (data.status) {
            if (data.recordCount > 0) {
                $(data.listofdata).each(function (index, value) {
                    fillCartBox(
                        value.itemDetailsid,
                        value.itemDetailsguid,
                        value.itemname,
                        value.itemqu,
                        value.itemimg,
                        value.itemprice,
                        value.itemTotal,
                        value.itemID
                    );
                });
            } else {
                $('.cart-dropdown-visible').addClass('d-none');
                $('.active-point').addClass('d-none');

                $('.cart-dropdown-empty').removeClass('d-none');
            }
        }

    });

}
function RemoveFromCart(productid) {
    $.ajax({
        type: "GET",
        url: "/site/home/RemoveItemFromCart",
        async: true,
        data: {
            _itemid: productid,
            _sessionID: getsessionID(),
        },

    }).done(function (data) {
        debugger;
        cartcount = cartcount - 1;

        $('.item-' + data + '').remove();
        $("#cancelpopUp").click();
        if (cartcount > 0) {
            $('.active-point-cart').show();
            $('.active-point-cart').html(cartcount);
        }
        else {
            $('.active-point-cart').hide();
            location.href = "/Site/Home/ViewCart";
        }
    });
}

function RemoveAllCart(NotRedirect) {
    cartcount = 0;
    $.ajax({
        type: "GET",
        url: "/site/home/RemoveAllItemCart",
        async: true,
        data: {
            _sessionID: getsessionID(),
        },

    }).done(function (data) {
        $('.cart-items-list').html("");
        $("#cancelpopUp").click();


        $('.cart-dropdown-empty').toggleClass('d-none');
        $('.cart-dropdown-visible').toggleClass('d-none');
        if (cartcount > 0) {
            $('.active-point-cart').show();
            $('.active-point-cart').html(cartcount);
        }
        else {
            $('.active-point-cart').hide();
        }
        if (NotRedirect != true) {
            location.href = "/Site/Home/ViewCart";

        }
    });
}

//#endregion


