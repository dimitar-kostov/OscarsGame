$(document).ready(function () {
		
	$.each($('.movieItem'), function (index, item) {
		$(item).find('.poster').attr('data-poster', index);
		$(item).find('.modal').attr('data-modal', index);
		$(item).find('.modal-content').attr('data-modalposter', index);
		$(item).find('.close').attr('data-close', index);
	});


	$(".poster").each(function (index, element) {
		var poster = $(this);
		//console.log(poster.data("poster"));
		poster.on("click", function () {
			imgSrc = this.src;
			var newImgSrc = imgSrc.replace("w300", "w500");
			//console.log(imgsrc);
			var index = $(this).data('poster');
			console.log(index);
			var modal = $('div[data-modal="' + index + '"]');
			var modalImg = $('[data-modalposter="' + index + '"]');
			var span = $('[data-close="' + index + '"]')[0];

			modal[0].style.display = "block";
			modalImg[0].src = newImgSrc;

			span.onclick = function () {
				modal[0].style.display = "none";
			}
		});
		
	});
	
	
})