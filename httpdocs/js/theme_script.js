/*----------------------------------------------------*/
/*	Scroll To Top Section
/*----------------------------------------------------*/
	jQuery(document).ready(function () {
	
		jQuery(window).scroll(function () {
			if (jQuery(this).scrollTop() > 100) {
				jQuery('.page_scrollup').fadeIn();
			} else {
				jQuery('.page_scrollup').fadeOut();
			}
		});
	
		jQuery('.page_scrollup').click(function () {
			jQuery("html, body").animate({
				scrollTop: 0
			}, 600);
			return false;
		});
	
	});	

	
	jQuery.browser = {};
			(function () {
				jQuery.browser.msie = false;
				jQuery.browser.version = 0;
				if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
					jQuery.browser.msie = true;
					jQuery.browser.version = RegExp.$1;
				}
			})();


// fix LeftMenu jQuery Toggle does not show on window resize
var leftmenu = jQuery('.nav-side-menu');
jQuery(window).on('resize', function () {    
	if ($(window).width() > 800) {
		leftmenu.show();		
    } 	
});

// Attach fancyBox when the document is loaded
$(document).ready(function ($) {
    $(".fancybox").fancybox();
});