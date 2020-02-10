var shape = [];
var shape_pixelart = [];
var shape_pixelart_coherence = [];

for(var i = 0; i < frame; i++){
	shape[i] = new Image();
	shape[i].src = "./PixelArt/" + id + "/Shape/" + id + "_ours_" + i + ".png";
	shape[i].onload =  ImageLoaded;
	
	shape_pixelart[i] = new Image();
	shape_pixelart[i].src = "./PixelArt/" + id + "/ShapePixelArt/" + id + "_ours_" + i + ".png";
	shape_pixelart[i].onload =  ImageLoaded;
	
	shape_pixelart_coherence[i] = new Image();
	shape_pixelart_coherence[i].src = "./PixelArt/" + id + "/Ours/" + id + "_ours_" + i + ".png";
	shape_pixelart_coherence[i].onload =  ImageLoaded;
}

var shape_img = document.getElementById("shape");
var shape_pixelart_img = document.getElementById("shape_pixelart");
var shape_pixelart_coherence_img = document.getElementById("shape_pixelart_coherence");
var frame_label = document.getElementById("current_frame");

function AssignFrame(new_index){
	shape_img.src=shape[new_index].src;
	shape_pixelart_img.src=shape_pixelart[new_index].src;
	shape_pixelart_coherence_img.src=shape_pixelart_coherence[new_index].src;
	document.getElementById("current_frame").innerHTML  = new_index;
}