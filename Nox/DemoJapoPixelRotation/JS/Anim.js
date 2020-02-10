////////////////////////////////////////////////////////////////////////////////////////
// Utility
////////////////////////////////////////////////////////////////////////////////////////
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
	results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

////////////////////////////////////////////////////////////////////////////////////////
// Side Navigation
////////////////////////////////////////////////////////////////////////////////////////
function openNav() {
	document.getElementById("mySidenav").style.width = "280px";
}

/* Set the width of the side navigation to 0 */
function closeNav() {
	document.getElementById("mySidenav").style.width = "0";
}

////////////////////////////////////////////////////////////////////////////////////////
// Animation Buttons
////////////////////////////////////////////////////////////////////////////////////////
function Play(){
	if (!playing){
		timer = setInterval(ChangePixelArt, 200);
		playing = true;
		document.getElementById("playbtn").style.display = "none";
		document.getElementById("lastbtn").style.display = "none";
		document.getElementById("nextbtn").style.display = "none";
		document.getElementById("pausebtn").style.display = "inline";
	}
	
}

function Pause(){
	if (playing){
		clearInterval(timer);
		playing = false;
		document.getElementById("playbtn").style.display = "inline";
		document.getElementById("lastbtn").style.display = "inline";
		document.getElementById("nextbtn").style.display = "inline";
		document.getElementById("pausebtn").style.display = "none";
	}
}

function LastStep(){
	if (!playing){
		current_index += frame-1;
		current_index %= frame;
		AssignFrame(current_index);		
	}
}

function NextStep(){
	if (!playing){
		current_index += 1;
		current_index %= frame;
		AssignFrame(current_index);		
	}
}

////////////////////////////////////////////////////////////////////////////////////////
// Keyboard
////////////////////////////////////////////////////////////////////////////////////////
document.onkeydown = function(e){
	
	if(e.keyCode == 32 /*Space*/){
		if(playing) Pause();
		else Play();
	}
	
	else if(e.keyCode == 37 /*left*/){
		LastStep()
	}
	
	else if(e.keyCode == 39 /*Right*/){
		NextStep();
	}
}
////////////////////////////////////////////////////////////////////////////////////////
// Animation Window
////////////////////////////////////////////////////////////////////////////////////////
var index = 0;
var id = images[0][0];
var frame =  images[0][1];
var name =  images[0][2];

var current_index = 0;

function ChangePixelArt(){
	current_index += 1;
	current_index %= frame;
	AssignFrame(current_index);		
}

// Get current animation id
var target = getParameterByName("Name") ;
if(target != null){ 
	for(var i = 0; i < images.length; i++)
	if(images[i][0] == target){
		index = i;
		id = images[i][0] ;
		frame = images[i][1];
		name = images[i][2];
		break;
	}
}

var count = 0;
var timer;
var playing = false;
// Check whether all images are loaded
function ImageLoaded(){
	count++;
	if(count ==  frame*2){
		document.getElementById("loading").style.display = 'none';
		Play();
	}
}

var introguide;
function InitialContent(){
	// Setup animation name
	document.getElementById("anim_name").innerHTML = index + " : " + name;
	
	// Get the current page
	var script = document.createElement('script');
	
	if(getParameterByName("Page") == "Comparison"){
		document.getElementById("comparisonpage").className += "topactive";
		script.src = "./JS/Comparison.js";
		document.getElementById("window").innerHTML = 
		'<table style="width:100%;">'+
		'<tr>'+
		'	<td class="pixelart">Nearest<img id="Nearest"></td>'+
		'	<td class="pixelart">Downsample<img id="Downsample"></td>'+
		'</tr>'+
		'<tr>'+
		'	<td class="pixelart">NPAR2013<img id="NPAR2013"></td>'+
		'	<td class="pixelart">Ours<img id="Ours"></td>'+
		'</tr>'+
		'</table>';
	}
	else if(getParameterByName("Page") == "Optimization"){
		document.getElementById("optimizationpage").className += "topactive";
		script.src = "./JS/Optimization.js";
		document.getElementById("window").innerHTML = 
		'<table style="width:100%;">'+
		'<tr>'+
		'	<td class="pixelart" width="50%">Shape Similarity<img id="shape"></td>'+
		'	<td class="pixelart" width="50%">Shape Similarity + Pixel Art Quality<img id="shape_pixelart"></td>'+
		'</tr>'+
		'<tr>'+
		'	<td class="pixelart" colspan="2">Shape Similarity + Pixel Art Quality + Temporal Coherence<img id="shape_pixelart_coherence"></td>'+
		'</tr>'+
		'</table>';
	}
	else{
		document.getElementById("pixelartpage").className += "topactive";
		script.src = "./JS/ShowPixelArt.js";
		document.getElementById("window").innerHTML = 
		'<table style="width:100%;">'+
		'<tr>'+
		'	<td class="pixelart" style="height:60vh">Color<img id="Color"></td>'+
		'	<td class="pixelart" style="height:60vh">Outline<img id="Outline"></td>'+
		'</tr>'+
		'</table>';
	}
	
	script.onload;
	document.head.appendChild(script);
	
	// Update Top Navigation Links
	var current_url = window.location.href.split('?')[0];
	
	document.getElementById("pixelartpage").href = current_url + "?Page=PixelArt";
	document.getElementById("comparisonpage").href = current_url + "?Page=Comparison";
	document.getElementById("optimizationpage").href = current_url + "?Page=Optimization";
	if(getParameterByName("Name") != null){
		document.getElementById("pixelartpage").href += "&Name=" + getParameterByName("Name");
		document.getElementById("comparisonpage").href += "&Name=" + getParameterByName("Name");
		document.getElementById("optimizationpage").href += "&Name=" + getParameterByName("Name");
	}
	
	// Insert Side Navigation Links
	var menu = document.getElementById("mySidenav");
	var page = getParameterByName("Page") != null ? "Page=" + getParameterByName("Page") + "&": "";
	for(var i = 0; i < images.length; i++){
		menu.innerHTML += "<a href='" + current_url + "?" + page + "Name=" + images[i][0] + "'>" + i + ": " + images[i][2] + "</a>";
	}
	menu.innerHTML += '<p style="margin-bottom: 2.5cm;">';
	
	////////////////////////////////////////////////////////////////////////////////////////
	// Website Tour
	////////////////////////////////////////////////////////////////////////////////////////
	
	introguide = introJs();
	introguide.setOption("showStepNumbers", false);
	introguide.setOptions({
		steps: [
		{
			element: '.topnav',
			intro: 'You can choose different result from here.',
			position: 'bottom',
		},
		{
			element: '#menu',
			intro: 'Click here to choose the animation you want to see.',
			position: 'bottom'
		},
		{
			element: '#show-frame',
			intro: 'The current frame of the animation.',
			position: 'top'
		},
		{
			element: '#media-button',
			intro: 'You can play or pause the animation from here.',
			position: 'top'
		},
		]
	});
	
}


