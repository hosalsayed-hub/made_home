/*------------- #General --------------*/

var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
});


$('.product-item .product-name a').matchHeight();
$('.product-item .product-details').matchHeight();
$('.blog-item .blog-title').matchHeight();
$('a[href="#"]').click(function ($) {
    $.preventDefault();
});


/*------------- #preloader   --------------*/

$(window).on("load", function () {

    $('.preloader').fadeOut();
    $('.preloader-spinner').delay(350).fadeOut('slow');

});


/*------------- #loading-overlay-btn function & add btn-cart and btn-wishlist action with loading-overlay --------------*/



$(".h-model").on('click', function () {

    var current_model = $(this).attr("data-bs-target");

    if ($(this).hasClass('with-delay')) {

        setTimeout(function () {

            $(current_model).addClass('show');

        }, 1000);

    } else {

        $(current_model).addClass('show');
    }

});
$('.modal-overlay').on('click', function () {


    $('.main-modal').removeClass('show');



});



function loading_overlay() {


    btn = $(this);
    btn.addClass('loading-overlay');
    if (btn.hasClass('btn-cart-wrap')) {

        btn.addClass('added');
    }
    setTimeout(function () {
        btn.removeClass('loading-overlay');
        btn.toggleClass('active')
    }, 1000);

}

/*
$('.loading-btn').click(function() {
    btn = $(this);
    btn.addClass('loading-overlay');
    setTimeout(function() {
      btn.removeClass('loading-overlay');
    }, 1000);
    
  });  
$('.btn-cart').click(function() {
    btn = $(this);
    btn.addClass('loading-overlay');
    btn.addClass('added');
    setTimeout(function() {
      btn.removeClass('loading-overlay');
      btn.addClass('active');
    }, 1000);
    
  });
$('.btn-wishlist').click(function() {
      
    btn = $(this);
    btn.addClass('loading-overlay');
    setTimeout(function() {
      btn.removeClass('loading-overlay');
      btn.toggleClass('active');
    }, 1000);

  });
*/

$('.btn-cart-wrap').click(loading_overlay);
$('.loading-btn').click(loading_overlay);


/*------------- #add btn-cart and btn-wishlist action without loading-overlay --------------*/

/*
$(".btn-cart").click(function(){
        
      $(this).addClass('active');
  });

*/
$(".btn-wishlist").click(function () {

    $(this).not('.not-wish').toggleClass('active');

});

/*------------- #hide and show elements using active class by clicking button ------------------------------------------
                elements: mobile-menu-wrapper, search-popup ,shop-sidebar   
----------------------------------------------------------------------------------------------------------------------*/

$('.sidebar-toggle').click(function () {

    $('.shop-sidebar').addClass('active');
});
$('.sidebar-close , .sidebar-overlay').click(function () {

    $('.shop-sidebar').removeClass('active');
});


$(".nav-toggler").click(function () {

    $('.mobile-menu-wrapper').addClass('active');
    $('.overlay-panel').addClass('active');

});
$(".overlay-panel , .close-menu ").click(function () {

    $('.mobile-menu-wrapper').removeClass('active');
    $(".overlay-panel").removeClass('active');

});


/*------------- #initialize Swiper sliders   --------------*/



/*
var swiper = new Swiper(".products-slider", {
    
       slidesPerView: 1,
       spaceBetween: 30,
       grabCursor: true,
       breakpoints: {
           
         320: {
           slidesPerView: 1,
           spaceBetween: 15,
           
         },
           
           
         420: {
           slidesPerView: 2,
           spaceBetween: 20,
         },
         
         768: {
           slidesPerView: 3,
           spaceBetween: 20,
         },
         992: {
           slidesPerView: 4,
           spaceBetween: 20,
         },
           
         1200: {
           slidesPerView: 4,
          
         },
       },
       navigation: {
         nextEl: ".swiper-btn-next",
         prevEl: ".swiper-btn-prev",
       },
       scrollbar: {
         el: ".swiper-scrollbar",
         hide: true,
       },
    
       
     });
*/

$(".products-slider").each(function (index, element) {
    var $this = $(this);
    $this.addClass("instance-" + index);
    $this.parent().find(".swiper-btn-prev").addClass("btn-prev-" + index);
    $this.parent().find(".swiper-btn-next").addClass("btn-next-" + index);
    var swiper = new Swiper(".instance-" + index, {

        slidesPerView: 1,
        spaceBetween: 30,
        grabCursor: true,
        breakpoints: {

            320: {
                slidesPerView: 1,
                spaceBetween: 15,

            },


            420: {
                slidesPerView: 2,
                spaceBetween: 20,
            },

            768: {
                slidesPerView: 3,
                spaceBetween: 20,
            },
            992: {
                slidesPerView: 4,
                spaceBetween: 20,
            },

            1200: {
                slidesPerView: 4,

            },
        },
        scrollbar: {
            el: ".swiper-scrollbar",
            hide: true,
        },
        navigation: {
            nextEl: ".btn-next-" + index,
            prevEl: ".btn-prev-" + index,
        },

    });
});
var swiper = new Swiper(".popular-provider-slider", {
    slidesPerView: 2,
    spaceBetween: 30,
    grabCursor: true,
    breakpoints: {

        320: {

            spaceBetween: 15,
        },
        480: {
            slidesPerView: 3,
            spaceBetween: 15,
        },


        768: {
            slidesPerView: 4,

        },
        992: {
            slidesPerView: 5,

        },

        1200: {
            slidesPerView: 6,

        },
    },


    autoplay: {
        delay: 2500,
        disableOnInteraction: true,
    },

    loop: true,



});
var swiper = new Swiper(".product-description-slider", {
    pagination: {
        el: ".swiper-pagination",
    },
    grabCursor: true,
});
var swiper = new Swiper(".related-blogs-sldier", {
    slidesPerView: 1,
    spaceBetween: 30,
    grabCursor: true,
    breakpoints: {

        576: {
            slidesPerView: 2,

        },
        768: {
            slidesPerView: 2,

        },
        992: {
            slidesPerView: 3,

        },

    },
    scrollbar: {
        el: ".swiper-scrollbar",
        hide: true,
    },
    navigation: {
        nextEl: ".swiper-btn-next",
        prevEl: ".swiper-btn-prev",
    },



});
var swiper = new Swiper(".welcome-slider", {

    slidesPerView: 1,
    spaceBetween: 30,
    mousewheel: false,
    grabCursor: true,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    autoplay: {
        delay: 2500,
        disableOnInteraction: false,
    },
});


/*------------- #sidebar fliters-accordion   --------------*/

$(function () {


    $(".panel-items .panel-header").click(function () {

        let $next = $(this).next(".panel-body");
        $(this).parent().toggleClass("active");
        $(this).next(".panel-body").slideToggle();
        $('.accordion-area .panel-items .panel-body').not($next).slideUp().parent().removeClass('active');

    });


});

/*------------- #login-tabs --------------*/

$(function () {

    $(".login-tab").hide();
    $('.login-tab.sign-in-tab').show();

    $(".tab-btn").click(function () {
        debugger;

        $(".tab-btn").removeClass("active");
        $(this).addClass("active");

        var current_tab = $(this).attr("data-target");
        $(".tab-content").hide();
        $("." + current_tab).fadeIn();
        //  if ($(this)[0].attributes.tab_prev.value !== "") {
        //if (checkStepValid($(this)[0].attributes["tap-id"].value)) {

        //        $(".tab-btn").removeClass("active");
        //        $(this).addClass("active");
        //        var current_tab = $(this).attr("data-target");
        //        $(".login-tab").hide();
        //        $("." + current_tab).fadeIn();
        //    }
        //}
        //else {
        //    if (checkStepValid($(this)[0].id, $(this)[0].attributes.tab_prev.value.tab_prev1)) {
        //        $(".tab-btn").removeClass("active");
        //        $(this).addClass("active");
        //        var current_tab = $(this).attr("data-target");
        //        $(".login-tab").hide();
        //        $("." + current_tab).fadeIn();
        //    }
        //}

    });
});


/*------------- #scroll-top btn   --------------*/


$(window).scroll(function () {

    if ($(this).scrollTop() > 200) {
        $('.scrollup').addClass("show")
    } else {
        $('.scrollup').removeClass("show");
        $('.scrollup').removeClass("active")

    }
});

$('.scrollup').click(function (e) {

    e.preventDefault();

    $(this).addClass('active');
    $('html,body').animate({

        scrollTop: 0
    }, 1200);
});



/*------------- #add active class on certain scroll point to make animation-------------------------------------
                elements: scroll-top-btn , tool-bar , actions-box
----------------------------------------------------------------------------------------------------------------*/

(function () {
    var documentElem = $(document);

    documentElem.on('scroll', function () {

        var currentScrollTop = $(this).scrollTop();

        if (currentScrollTop > 230) {

            $('.scroll-top').addClass('active');
            $('.tool-bar').addClass('active');
            $('.actions-box').addClass('active');
        }

        else {

            $('.scroll-top').removeClass('active');
            $('.tool-bar').removeClass('active');
            $('.actions-box').removeClass('active');
        }


    });

})();


/*------------- #Add Plus Minus Button To Input Number   --------------*/

/*------------- Add Plus Minus Button To Input Number function-1   --------------*/

/*
         $('.count-sub').click(function () {
                var $input = $(this).parent().find('input');
                var count = parseInt($input.val()) - 1;
                count = count < 1 ? 1 : count;
                $input.val(count);
                $input.change();
                
                if(parseInt($input.val()) <= parseInt($input.attr('min')) ) {
                    $(this).addClass("disabled")
                }else{
                    
                    $(this).removeClass("disabled")
                }
             $('.count-add').removeClass('disabled')
            });

            $('.count-add').click(function () {
                var $input = $(this).parent().find('input');
                $input.val(parseInt($input.val()) + 1);
                $input.change();
                if(parseInt($input.val()) >= parseInt($input.attr('max')) ) {
                 $(this).addClass("disabled")
                }else{
                    
                     $(this).removeClass("disabled")
                }
                $('.count-sub').removeClass('disabled')
            });

*/

/*------------- Add Plus Minus Button To Input Number function-2   --------------*/

$('.count-btn').click(function (e) {
    e.preventDefault();

    fieldName = $(this).attr('data-field');
    type = $(this).attr('data-type');
    var input = $("input[name='" + fieldName + "']");
    var currentVal = parseInt(input.val());
    if (!isNaN(currentVal)) {
        if (type == 'minus') {

            if (currentVal > input.attr('min')) {
                input.val(currentVal - 1).change();
            }
            if (parseInt(input.val()) == input.attr('min')) {
                $(this).addClass('disabled')
            }
            if (input.attr('data-quantity') != "" && input.attr('data-quantity') != undefined) {
                if (parseInt(input.val()) > input.attr('data-quantity')) {
                    $(".count-quantity[name='" + fieldName + "']").show();
                    input.attr("data-isshowbtn", "1");
                }
                else {
                    $(".count-quantity[name='" + fieldName + "']").hide();
                    input.attr("data-isshowbtn", "0");
                }
            }
            $('.count-add').removeClass('disabled');

        }
        else if (type == 'plus') {
            if (currentVal < input.attr('max')) {
                input.val(currentVal + 1).change();
            }
            if (parseInt(input.val()) == input.attr('max')) {
                $(this).addClass('disabled')
            }
            if (input.attr('data-quantity') != "" && input.attr('data-quantity') != undefined) {
                if (parseInt(input.val()) > input.attr('data-quantity')) {
                    $(".count-quantity[name='" + fieldName + "']").show();
                    input.attr("data-isshowbtn", "1");
                }
                else {
                    $(".count-quantity[name='" + fieldName + "']").hide();
                    input.attr("data-isshowbtn", "0");
                }
            }
            $('.count-sub').removeClass('disabled')
        }
        if ($('.count-num[data-isshowbtn="1"]')[0] !== undefined) {
            $(".increase_order").show();
            $(".complete-purchase").hide();
        }
        else {
            $(".increase_order").hide();
            $(".complete-purchase").show();
        }

    } else {
        input.val(0);
    }
});
$('.count-num').focusin(function () {
    $(this).data('oldValue', $(this).val());
});
$('.count-num').change(function () {
    minValue = parseInt($(this).attr('min'));
    maxValue = parseInt($(this).attr('max'));
    valueCurrent = parseInt($(this).val());

    fieldName = $(this).attr("name");

    name = $(this).attr('name');
    if (valueCurrent >= minValue) {
        $(".btn-number[data-type='minus'][data-field='" + name + "']").removeClass('disabled')
    } else {
        alert('Sorry, the minimum value was reached');
        $(this).val($(this).data('oldValue'));
    }
    if (valueCurrent <= maxValue) {
        $(".btn-number[data-type='plus'][data-field='" + name + "']").removeClass('disabled')
    } else {
        alert('Sorry, the maximum value was reached');
        $(this).val($(this).data('oldValue'));
    }
    if ($(this).attr('data-quantity') != "" && $(this).attr('data-quantity') != undefined) {
        if (valueCurrent > $(this).attr('data-quantity')) {
            $(".count-quantity[name='" + fieldName + "']").show();
            $(this).attr("data-isshowbtn", "1");
        }
        else {
            $(".count-quantity[name='" + fieldName + "']").hide();
            $(this).attr("data-isshowbtn", "0");
        }
    }
    if ($('.count-num[data-isshowbtn="1"]')[0] !== undefined) {
        $(".increase_order").show();
        $(".complete-purchase").hide();
    }
    else {
        $(".increase_order").hide();
        $(".complete-purchase").show();
    }

});
$(".count-num").keydown(function (e) {
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 190]) !== -1 ||
        // Allow: Ctrl+A
        (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});
/*******************************/

$(".tab-content").hide();
$(".tab-content.active").show();

$(".tab-btn").click(function () {
    debugger;

    // if ($(this)[0].attributes.tab_prev.value !== "" ) {
    $(".tab-btn").removeClass("active");
    $(this).addClass("active");

    var current_tab = $(this).attr("data-target");
    $(".tab-content").hide();
    $("." + current_tab).fadeIn();

    //if (checkStepValid($(this)[0].attributes["tap-id"].value)) {

    //    $(".tab-btn").removeClass("active");
    //    $(this).addClass("active");

    //    var current_tab = $(this).attr("data-target");
    //    $(".tab-content").hide();
    //    $("." + current_tab).fadeIn();
    //}
    //  }

    //else {
    //    if (checkStepValid($(this)[0].id, $(this)[0].attributes.tab_prev1.value)) {

    //        $(".tab-btn").removeClass("active");
    //        $(this).addClass("active");

    //        var current_tab = $(this).attr("data-target");
    //        $(".tab-content").hide();
    //        $("." + current_tab).fadeIn();
    //    }
    //}


});



$('.password-field .eye-icon').on('click', function () {


    var password_input = $(this).parent().find(".password-input");
    console.log(password_input);

    if (password_input.attr('type') === 'password') {

        password_input.attr('type', 'text');
        $(this).addClass('hide');

    } else {

        password_input.attr('type', 'password');
        $(this).removeClass('hide');
    }


});



//(function () {
//                var documentElem = $(document),
//                        nav = $('.top-nav'),
//                        lastScrollTop = 0;

//                documentElem.on('scroll', function () {
//                    var currentScrollTop = $(this).scrollTop();

//                    //scroll down
//                    if ( currentScrollTop > lastScrollTop && !$(".mobile-menu-wrapper").hasClass("active") && 
//                        !$('.h-menu').hasClass("show"))

//                        nav.addClass('scroll');
//                    //scroll up
//                    else
//                        nav.removeClass('scroll');

//                    lastScrollTop = currentScrollTop;
//                });

//})();


/* -------------------------------
    * Header dropdowns
    * ------------------------------- */

$(".h-menu").click(function () {

    var current_tab = $(this).parent().attr("data-target");

    if ($(this).hasClass('show')) {

        $(this).removeClass("show");
        $("." + current_tab).removeClass("show");

    }
    else {


        $(".h-menu").removeClass("show");
        $(this).addClass("show");

        $(".dropdown-list").removeClass("show");
        $("." + current_tab).addClass("show");

    }


});

$('.close-cart , .mobile-cart-overlay ').click(function () {

    $(".cart-dropdown").removeClass("show");
    $(".mobile-cart-overlay").removeClass("active");
    $('.tool-link.cart-icon').removeClass("show");

});

$('.tool-link.cart-icon').click(function () {


    if ($(this).hasClass('show')) {

        $(".mobile-cart-overlay").addClass("active");

    }
    else {

        $(".mobile-cart-overlay").removeClass("active");
    }

});






$(document).click(function (e) {

    if (!(($(e.target).closest('.dropdown-list').length > 0) ||

        ($(e.target).closest('.h-menu').length > 0))) {

        $(".h-menu").removeClass("show");
        $(".dropdown-list").removeClass("show");
    }

});


$(".toggle-modal-btn").click(function () {

    var this_modal = $(this).closest('.custom-bootstrap-modal');
    $(this_modal).modal('hide');

});

