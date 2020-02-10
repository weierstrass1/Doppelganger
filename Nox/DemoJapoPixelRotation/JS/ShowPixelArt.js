var color = [];
var outline = [];

for(var i = 0; i < frame; i++){
	color[i] = new Image();
	color[i].src = "./PixelArt/" + id + "/Color/" + id + "_" + i + ".png";
	color[i].onload = ImageLoaded;
	
	outline[i] = new Image();
	outline[i].src = "./PixelArt/" + id + "/Ours/" + id + "_ours_" + i + ".png";
	outline[i].onload = ImageLoaded;
}

var color_img = document.getElementById("Color");
var outline_img = document.getElementById("Outline");
var frame_label = document.getElementById("current_frame");

function AssignFrame(new_index){
	color_img.src=color[new_index].src;
	outline_img.src=outline[new_index].src;
	frame_label.innerHTML  = new_index;
}