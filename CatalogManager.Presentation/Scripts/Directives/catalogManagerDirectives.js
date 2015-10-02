angular.module('catalogManager.uiDirectives')
        .factory('pageLayout', function () {
            return {
                setFooterBottom: function () {
                    var screenHeight = $(window).height(),
                        minHeight = screenHeight - (50 + 8) /* top header height */ - (70 + 8) /* footer height */,
                        menuHeight = $('.body-menu-container').height(),
                        adjustmentHeight = minHeight > menuHeight ? minHeight : menuHeight;

                    $('.body-content').css({
                        'min-height': adjustmentHeight + 'px'
                    });
                }
            }
        })
        .directive('hubPopupover', [function () {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {
                    var elementTop = parseInt($(element).css('top'), 10);
                    var throttle = null;
                    $(window).on('scroll', function () {
                        if (throttle) {
                            clearTimeout(throttle);
                            throttle = null;
                        }
                        throttle = setTimeout(function () {
                            var top = $(window).scrollTop();
                            var top2 = elementTop - top;
                            $(element).css('top', top2 + 'px');
                        }, 40);
                    });
                }
            }
        }])
        .directive('hubBackTop', [function () {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {
                    var throttle = null;
                    $(window).on('scroll', function () {
                        if (throttle) {
                            clearTimeout(throttle);
                            throttle = null;
                        }
                        throttle = setTimeout(function () {
                            var top = $(window).scrollTop();
                            if (top > 0)
                                $(element).show();
                            else
                                $(element).hide();
                        }, 100);
                    });
                }
            }
        }])
        .directive('hubFooter', ['pageLayout', function (pageLayout) {
            return {
                restrict: 'A',
                link: function (scope, element, attrs) {
                    var throttle = null;
                    $(window).on('resize', function () {
                        if (throttle) {
                            clearTimeout(throttle);
                            throttle = null;
                        }
                        throttle = setTimeout(function () {
                            pageLayout.setFooterBottom();
                        }, 100);
                    });

                    pageLayout.setFooterBottom();
                }
            }
        }])
        .directive('hubMenuToggle', ['$location', 'pageLayout', function ($location, pageLayout) {
            return {
                restrict: 'A',
                link: function (scope, element, attr) {
                    $(element).on('click', function () {
                        scope.togglemenu = !scope.togglemenu;
                        $('.body-container').toggleClass('menu-collapsed');
                        $(element).find('span.fa').toggleClass('fa-caret-left').toggleClass('fa-caret-right');
                        pageLayout.setFooterBottom();
                        window.scrollTo(0, 0);
                    });
                    $(window).on('scroll', function () {
                        var top = $(window).scrollTop();
                        if (top > 50)
                            $(element).addClass('top');
                        else
                            $(element).removeClass('top');
                    });
                }
            }
        }])
        .directive('hubSubmenuToggle', ['pageLayout', function (pageLayout) {
            return {
                restrict: 'A',
                link: function (scope, element, attr) {
                    var isCaret = $(element).is('i.fa');
                    // Toggles or sets the openness state of the submenu
                    // state can be left undefined, in which case function will toggle visibility
                    // state set to true will show the submenu, false will hide the submenu
                    // We rely on second optional toggleClass parameter, which allows to add/remove a class
                    // when it is true/false.
                    var showHide = function (visibility) {

                        if ($(element).parents('.nav-second-level.nav-show').length) return;
                        $('#menuMain .fa-caret-down').removeClass('unhighlight');
                        var submenu = $(element).nextAll('ul').eq(0);
                        if (!submenu.hasClass('nav-show')) {
                            $('#menuMain .nav-show').toggleClass('nav-show');
                            $('#menuMain .fa-caret-down')
                                .removeClass('fa-caret-down')
                                .addClass('fa-caret-right');
                        }

                        var caret = $(element).is('i.fa') ? $(element) : $(element).parent().find('i.fa').eq(0);
                        submenu.toggleClass('nav-show', visibility);
                        caret.toggleClass('fa-caret-down', visibility)
                        caret.toggleClass('fa-caret-right', (visibility === undefined) ? undefined : !visibility);

                        // there is 500 ms in css transition animation
                        var throttle = null;
                        if (throttle) {
                            clearTimeout(throttle);
                            throttle = null;
                        }
                        throttle = setTimeout(function () {
                            pageLayout.setFooterBottom();
                        }, 500);
                    };
                    $(element).on('click', function () {
                        var visibility = isCaret ? undefined : true;
                        return showHide(visibility);
                    });
                }
            }
        }]);
