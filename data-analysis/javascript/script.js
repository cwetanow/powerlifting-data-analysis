$(document).ready(function () {
  $('.navigation-btn').click(function () {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
      && location.hostname == this.hostname) {
      var $target = $(this.hash);
      $target = $target.length && $target
        || $('[name=' + this.hash.slice(1) + ']');
      if ($target.length) {
        var targetOffset = $target.offset().top - 50;
        $('html,body')
          .animate({ scrollTop: targetOffset }, 1000);
        return true;
      }
    }
  });

  $('#sidebar-wrapper a').click(function () {
    $('.active').removeClass('active');
    $(this).addClass('active');
  });
});