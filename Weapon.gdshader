shader_type canvas_item;
// 创建描边的shader

//描边颜色
uniform vec4 line_color : source_color = vec4(0,0,0,1);
//厚度
uniform float line_thickness : hint_range(0.0, 10, 0.1) = 1.0;

const vec2 OFFSET[8] = {
	vec2(-1,-1),vec2(-1,0),vec2(-1,1),vec2(0,-1),vec2(0,1),
	vec2(1,-1),vec2(1,0),vec2(1,1)
};

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
vec2 size = TEXTURE_PIXEL_SIZE * line_thickness;
float outline = 0.0;

for(int i = 0;i <OFFSET.length();i++){
	//.a是透明度信息
	outline += texture(TEXTURE,UV + size*OFFSET[i]).a;
}
outline = min(outline,1.0);
vec4 color = texture(TEXTURE,UV);
COLOR = mix(color,line_color,outline-color.a);


}

void light() {
	// Called for every pixel for every light affecting the CanvasItem.
}
