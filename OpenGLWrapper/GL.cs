using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLWrapper
{
    public static unsafe class GL
    {
        // ######################################################################################################

        [DllImport("opengl32.dll", EntryPoint = "wglGetProcAddress", SetLastError = true)]
        private static extern IntPtr wglGetProcAddress(string name);
        // Import GetProcAddress from kernel32.dll to load functions from opengl32.dll
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // ######################################################################################################


        // ####################################### Delegates #######################################

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glClear_t(ClearBufferMask mask);
        private static readonly Lazy<glClear_t> _Clear = new Lazy<glClear_t>(() => LoadFunction<glClear_t>("glClear"));
        public static glClear_t Clear => _Clear.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGenVertexArrays_t(uint n, out uint arrays);
        private static readonly Lazy<glGenVertexArrays_t> _GenVertexArrays = new Lazy<glGenVertexArrays_t>(() => LoadFunction<glGenVertexArrays_t>("glGenVertexArrays"));
        public static glGenVertexArrays_t GenVertexArrays => _GenVertexArrays.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetError_t();
        private static readonly Lazy<glGetError_t> _GetError = new Lazy<glGetError_t>(() => LoadFunction<glGetError_t>("glGetError"));
        public static glGetError_t GetError => _GetError.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindVertexArray_t(uint vao);
        private static readonly Lazy<glBindVertexArray_t> _BindVertexArray = new Lazy<glBindVertexArray_t>(() => LoadFunction<glBindVertexArray_t>("glBindVertexArray"));
        public static glBindVertexArray_t BindVertexArray => _BindVertexArray.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindTexture_t(TextureTarget target, uint texture);
        private static readonly Lazy<glBindTexture_t> _BindTexture =
            new Lazy<glBindTexture_t>(() => LoadFunction<glBindTexture_t>("glBindTexture"));
        public static glBindTexture_t BindTexture => _BindTexture.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glClearColor_t(float r, float g, float b, float a);
        private static readonly Lazy<glClearColor_t> _ClearColor = new Lazy<glClearColor_t>(() => LoadFunction<glClearColor_t>("glClearColor"));
        public static glClearColor_t ClearColor => _ClearColor.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDrawElements_t(PrimitiveType mode, uint count, DrawElementsType type, void* indices);
        private static readonly Lazy<glDrawElements_t> _DrawElements = new Lazy<glDrawElements_t>(() => LoadFunction<glDrawElements_t>("glDrawElements"));
        public static glDrawElements_t DrawElements => _DrawElements.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDrawArrays_t(PrimitiveType mode, int first, uint count);
        private static readonly Lazy<glDrawArrays_t> _DrawArrays = new Lazy<glDrawArrays_t>(() => LoadFunction<glDrawArrays_t>("glDrawArrays"));
        public static glDrawArrays_t DrawArrays => _DrawArrays.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGenBuffers_t(uint n, out uint buffer);
        private static readonly Lazy<glGenBuffers_t> _GenBuffers = new Lazy<glGenBuffers_t>(() => LoadFunction<glGenBuffers_t>("glGenBuffers"));
        public static glGenBuffers_t GenBuffers => _GenBuffers.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDeleteBuffers_t(uint n, ref uint buffer);
        private static readonly Lazy<glDeleteBuffers_t> _DeleteBuffers = new Lazy<glDeleteBuffers_t>(() => LoadFunction<glDeleteBuffers_t>("glDeleteBuffers"));
        public static glDeleteBuffers_t DeleteBuffers => _DeleteBuffers.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDispatchCompute_t(uint numGroupsX, uint numGroupsY, uint numGroupsZ);
        private static readonly Lazy<glDispatchCompute_t> _DispatchCompute =
            new Lazy<glDispatchCompute_t>(() => LoadFunction<glDispatchCompute_t>("glDispatchCompute"));
        public static glDispatchCompute_t DispatchCompute => _DispatchCompute.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glMemoryBarrier_t(MemoryBarrierFlags barriers);
        private static readonly Lazy<glMemoryBarrier_t> _MemoryBarrier =
            new Lazy<glMemoryBarrier_t>(() => LoadFunction<glMemoryBarrier_t>("glMemoryBarrier"));

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindImageTexture_t(
        uint unit,
        uint texture,
        int level,
        GLboolean layered,
        int layer,
        TextureAccess access,
        SizedInternalFormat format
    );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glTexBuffer_t(TextureTarget target, SizedInternalFormat internalformat, uint buffer);
        private static readonly Lazy<glTexBuffer_t> _TexBuffer =
            new Lazy<glTexBuffer_t>(() => LoadFunction<glTexBuffer_t>("glTexBuffer"));
        public static glTexBuffer_t TexBuffer => _TexBuffer.Value;

        private static readonly Lazy<glBindImageTexture_t> _BindImageTexture =
            new Lazy<glBindImageTexture_t>(() => LoadFunction<glBindImageTexture_t>("glBindImageTexture"));
        public static glBindImageTexture_t BindImageTexture => _BindImageTexture.Value;
        public static glMemoryBarrier_t MemoryBarrier => _MemoryBarrier.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glActiveTexture_t(TextureUnit texture);
        private static readonly Lazy<glActiveTexture_t> _ActiveTexture = new Lazy<glActiveTexture_t>(() => LoadFunction<glActiveTexture_t>("glActiveTexture"));
        public static glActiveTexture_t ActiveTexture => _ActiveTexture.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGenTextures_t(uint n, out uint textures);
        private static readonly Lazy<glGenTextures_t> _GenTextures = new Lazy<glGenTextures_t>(() => LoadFunction<glGenTextures_t>("glGenTextures"));
        public static glGenTextures_t GenTextures => _GenTextures.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDeleteTextures_t(uint n, ref uint textures);
        private static readonly Lazy<glDeleteTextures_t> _DeleteTextures = new Lazy<glDeleteTextures_t>(() => LoadFunction<glDeleteTextures_t>("glDeleteTextures"));
        public static glDeleteTextures_t DeleteTextures => _DeleteTextures.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindBuffer_t(BufferTarget target, uint buffer);
        private static readonly Lazy<glBindBuffer_t> _BindBuffer = new Lazy<glBindBuffer_t>(() => LoadFunction<glBindBuffer_t>("glBindBuffer"));
        public static glBindBuffer_t BindBuffer => _BindBuffer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glViewport_t(int x, int y, uint width, uint height);
        private static readonly Lazy<glViewport_t> _Viewport = new Lazy<glViewport_t>(() => LoadFunction<glViewport_t>("glViewport"));
        public static glViewport_t Viewport => _Viewport.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBufferSubData_t(BufferTarget target, IntPtr offset, UIntPtr size, void* data);
        private static readonly Lazy<glBufferSubData_t> _BufferSubData = new Lazy<glBufferSubData_t>(() => LoadFunction<glBufferSubData_t>("glBufferSubData"));
        public static glBufferSubData_t BufferSubData => _BufferSubData.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glNamedBufferSubData_t(uint buffer, IntPtr offset, uint size, void* data);
        private static readonly Lazy<glNamedBufferSubData_t> _NamedBufferSubData = new Lazy<glNamedBufferSubData_t>(() => LoadFunction<glNamedBufferSubData_t>("glNamedBufferSubData"));
        public static glNamedBufferSubData_t NamedBufferSubData => _NamedBufferSubData.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glScissor_t(int x, int y, uint width, uint height);
        private static readonly Lazy<glScissor_t> _Scissor = new Lazy<glScissor_t>(() => LoadFunction<glScissor_t>("glScissor"));
        public static glScissor_t Scissor => _Scissor.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glTexSubImage2D_t(
            TextureTarget target,
            int level,
            int xoffset,
            int yoffset,
            uint width,
            uint height,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels);
        private static readonly Lazy<glTexSubImage2D_t> _TexSubImage2D = new Lazy<glTexSubImage2D_t>(() => LoadFunction<glTexSubImage2D_t>("glTexSubImage2D"));
        public static glTexSubImage2D_t TexSubImage2D => _TexSubImage2D.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glShaderSource_t(uint shader, uint count, byte** @string, int* length);
        private static readonly Lazy<glShaderSource_t> _ShaderSource = new Lazy<glShaderSource_t>(() => LoadFunction<glShaderSource_t>("glShaderSource"));
        public static glShaderSource_t ShaderSource => _ShaderSource.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate uint glCreateShader_t(ShaderType shaderType);
        private static readonly Lazy<glCreateShader_t> _CreateShader = new Lazy<glCreateShader_t>(() => LoadFunction<glCreateShader_t>("glCreateShader"));
        public static glCreateShader_t CreateShader => _CreateShader.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glCompileShader_t(uint shader);
        private static readonly Lazy<glCompileShader_t> _CompileShader = new Lazy<glCompileShader_t>(() => LoadFunction<glCompileShader_t>("glCompileShader"));
        public static glCompileShader_t CompileShader => _CompileShader.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetShaderiv_t(uint shader, ShaderParameter pname, int* @params);
        private static readonly Lazy<glGetShaderiv_t> _GetShaderiv = new Lazy<glGetShaderiv_t>(() => LoadFunction<glGetShaderiv_t>("glGetShaderiv"));
        public static glGetShaderiv_t GetShaderiv => _GetShaderiv.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetShaderInfoLog_t(uint shader, uint maxLength, uint* length, byte* infoLog);
        private static readonly Lazy<glGetShaderInfoLog_t> _GetShaderInfoLog = new Lazy<glGetShaderInfoLog_t>(() => LoadFunction<glGetShaderInfoLog_t>("glGetShaderInfoLog"));
        public static glGetShaderInfoLog_t GetShaderInfoLog => _GetShaderInfoLog.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDeleteShader_t(uint shader);
        private static readonly Lazy<glDeleteShader_t> _DeleteShader = new Lazy<glDeleteShader_t>(() => LoadFunction<glDeleteShader_t>("glDeleteShader"));
        public static glDeleteShader_t DeleteShader => _DeleteShader.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGenSamplers_t(uint n, out uint samplers);
        private static readonly Lazy<glGenSamplers_t> _GenSamplers = new Lazy<glGenSamplers_t>(() => LoadFunction<glGenSamplers_t>("glGenSamplers"));
        public static glGenSamplers_t GenSamplers => _GenSamplers.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glSamplerParameterf_t(uint sampler, SamplerParameterName pname, float param);
        private static readonly Lazy<glSamplerParameterf_t> _SamplerParameterf = new Lazy<glSamplerParameterf_t>(() => LoadFunction<glSamplerParameterf_t>("glSamplerParameterf"));
        public static glSamplerParameterf_t SamplerParameterf => _SamplerParameterf.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glSamplerParameteri_t(uint sampler, SamplerParameterName pname, int param);
        private static readonly Lazy<glSamplerParameteri_t> _SamplerParameteri = new Lazy<glSamplerParameteri_t>(() => LoadFunction<glSamplerParameteri_t>("glSamplerParameteri"));
        public static glSamplerParameteri_t SamplerParameteri => _SamplerParameteri.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glSamplerParameterfv_t(uint sampler, SamplerParameterName pname, float* @params);
        private static readonly Lazy<glSamplerParameterfv_t> _SamplerParameterfv = new Lazy<glSamplerParameterfv_t>(() => LoadFunction<glSamplerParameterfv_t>("glSamplerParameterfv"));
        public static glSamplerParameterfv_t SamplerParameterfv => _SamplerParameterfv.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindSampler_t(uint unit, uint sampler);
        private static readonly Lazy<glBindSampler_t> _BindSampler = new Lazy<glBindSampler_t>(() => LoadFunction<glBindSampler_t>("glBindSampler"));
        public static glBindSampler_t BindSampler => _BindSampler.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDeleteSamplers_t(uint n, ref uint samplers);
        private static readonly Lazy<glDeleteSamplers_t> _DeleteSamplers = new Lazy<glDeleteSamplers_t>(() => LoadFunction<glDeleteSamplers_t>("glDeleteSamplers"));
        public static glDeleteSamplers_t DeleteSamplers => _DeleteSamplers.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glColorMask_t(
            GLboolean red,
            GLboolean green,
            GLboolean blue,
            GLboolean alpha);
        private static readonly Lazy<glColorMask_t> _ColorMask = new Lazy<glColorMask_t>(() => LoadFunction<glColorMask_t>("glColorMask"));
        public static glColorMask_t ColorMask => _ColorMask.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glColorMaski_t(
            uint buf,
            GLboolean red,
            GLboolean green,
            GLboolean blue,
            GLboolean alpha);
        private static readonly Lazy<glColorMaski_t> _ColorMaski = new Lazy<glColorMaski_t>(() => LoadFunction<glColorMaski_t>("glColorMaski"));
        public static glColorMaski_t ColorMaski => _ColorMaski.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBlendFuncSeparatei_t(
            uint buf,
            BlendingFactorSrc srcRGB,
            BlendingFactorDest dstRGB,
            BlendingFactorSrc srcAlpha,
            BlendingFactorDest dstAlpha);
        private static readonly Lazy<glBlendFuncSeparatei_t> _BlendFuncSeparatei = new Lazy<glBlendFuncSeparatei_t>(() => LoadFunction<glBlendFuncSeparatei_t>("glBlendFuncSeparatei"));
        public static glBlendFuncSeparatei_t BlendFuncSeparatei => _BlendFuncSeparatei.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBlendFuncSeparate_t(
            BlendingFactorSrc srcRGB,
            BlendingFactorDest dstRGB,
            BlendingFactorSrc srcAlpha,
            BlendingFactorDest dstAlpha);
        private static readonly Lazy<glBlendFuncSeparate_t> _BlendFuncSeparate = new Lazy<glBlendFuncSeparate_t>(() => LoadFunction<glBlendFuncSeparate_t>("glBlendFuncSeparate"));
        public static glBlendFuncSeparate_t BlendFuncSeparate => _BlendFuncSeparate.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glEnable_t(EnableCap cap);
        private static readonly Lazy<glEnable_t> _Enable = new Lazy<glEnable_t>(() => LoadFunction<glEnable_t>("glEnable"));
        public static glEnable_t Enable => _Enable.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glEnablei_t(EnableCap cap, uint index);
        private static readonly Lazy<glEnablei_t> _Enablei = new Lazy<glEnablei_t>(() => LoadFunction<glEnablei_t>("glEnablei"));
        public static glEnablei_t Enablei => _Enablei.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDisable_t(EnableCap cap);
        private static readonly Lazy<glDisable_t> _Disable = new Lazy<glDisable_t>(() => LoadFunction<glDisable_t>("glDisable"));
        public static glDisable_t Disable => _Disable.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDisablei_t(EnableCap cap, uint index);
        private static readonly Lazy<glDisablei_t> _Disablei = new Lazy<glDisablei_t>(() => LoadFunction<glDisablei_t>("glDisablei"));
        public static glDisablei_t Disablei => _Disablei.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBlendEquationSeparatei_t(uint buf, BlendEquationMode modeRGB, BlendEquationMode modeAlpha);
        private static readonly Lazy<glBlendEquationSeparatei_t> _BlendEquationSeparatei = new Lazy<glBlendEquationSeparatei_t>(() => LoadFunction<glBlendEquationSeparatei_t>("glBlendEquationSeparatei"));
        public static glBlendEquationSeparatei_t BlendEquationSeparatei => _BlendEquationSeparatei.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBlendEquationSeparate_t(BlendEquationMode modeRGB, BlendEquationMode modeAlpha);
        private static readonly Lazy<glBlendEquationSeparate_t> _BlendEquationSeparate = new Lazy<glBlendEquationSeparate_t>(() => LoadFunction<glBlendEquationSeparate_t>("glBlendEquationSeparate"));
        public static glBlendEquationSeparate_t BlendEquationSeparate => _BlendEquationSeparate.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBlendColor_t(float red, float green, float blue, float alpha);
        private static readonly Lazy<glBlendColor_t> _BlendColor = new Lazy<glBlendColor_t>(() => LoadFunction<glBlendColor_t>("glBlendColor"));
        public static glBlendColor_t BlendColor => _BlendColor.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDepthFunc_t(DepthFunction func);
        private static readonly Lazy<glDepthFunc_t> _DepthFunc = new Lazy<glDepthFunc_t>(() => LoadFunction<glDepthFunc_t>("glDepthFunc"));
        public static glDepthFunc_t DepthFunc => _DepthFunc.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDepthMask_t(GLboolean flag);
        private static readonly Lazy<glDepthMask_t> _DepthMask = new Lazy<glDepthMask_t>(() => LoadFunction<glDepthMask_t>("glDepthMask"));
        public static glDepthMask_t DepthMask => _DepthMask.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glCullFace_t(CullFaceMode mode);
        private static readonly Lazy<glCullFace_t> _CullFace = new Lazy<glCullFace_t>(() => LoadFunction<glCullFace_t>("glCullFace"));
        public static glCullFace_t CullFace => _CullFace.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glPolygonMode_t(MaterialFace face, PolygonMode mode);
        private static readonly Lazy<glPolygonMode_t> _PolygonMode = new Lazy<glPolygonMode_t>(() => LoadFunction<glPolygonMode_t>("glPolygonMode"));
        public static glPolygonMode_t PolygonMode => _PolygonMode.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate uint glCreateProgram_t();
        private static readonly Lazy<glCreateProgram_t> _CreateProgram = new Lazy<glCreateProgram_t>(() => LoadFunction<glCreateProgram_t>("glCreateProgram"));
        public static glCreateProgram_t CreateProgram => _CreateProgram.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glAttachShader_t(uint program, uint shader);
        private static readonly Lazy<glAttachShader_t> _AttachShader = new Lazy<glAttachShader_t>(() => LoadFunction<glAttachShader_t>("glAttachShader"));
        public static glAttachShader_t AttachShader => _AttachShader.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindAttribLocation_t(uint program, uint index, byte* name);
        private static readonly Lazy<glBindAttribLocation_t> _BindAttribLocation = new Lazy<glBindAttribLocation_t>(() => LoadFunction<glBindAttribLocation_t>("glBindAttribLocation"));
        public static glBindAttribLocation_t BindAttribLocation => _BindAttribLocation.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glLinkProgram_t(uint program);
        private static readonly Lazy<glLinkProgram_t> _LinkProgram = new Lazy<glLinkProgram_t>(() => LoadFunction<glLinkProgram_t>("glLinkProgram"));
        public static glLinkProgram_t LinkProgram => _LinkProgram.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetProgramiv_t(uint program, GetProgramParameterName pname, int* @params);
        private static readonly Lazy<glGetProgramiv_t> _GetProgramiv = new Lazy<glGetProgramiv_t>(() => LoadFunction<glGetProgramiv_t>("glGetProgramiv"));
        public static glGetProgramiv_t GetProgramiv => _GetProgramiv.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetProgramInfoLog_t(uint program, uint maxLength, uint* length, byte* infoLog);
        private static readonly Lazy<glGetProgramInfoLog_t> _GetProgramInfoLog = new Lazy<glGetProgramInfoLog_t>(() => LoadFunction<glGetProgramInfoLog_t>("glGetProgramInfoLog"));
        public static glGetProgramInfoLog_t GetProgramInfoLog => _GetProgramInfoLog.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDeleteProgram_t(uint program);
        private static readonly Lazy<glDeleteProgram_t> _DeleteProgram = new Lazy<glDeleteProgram_t>(() => LoadFunction<glDeleteProgram_t>("glDeleteProgram"));
        public static glDeleteProgram_t DeleteProgram => _DeleteProgram.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUniform1i_t(int location, int v0);
        private static readonly Lazy<glUniform1i_t> _Uniform1i = new Lazy<glUniform1i_t>(() => LoadFunction<glUniform1i_t>("glUniform1i"));
        public static glUniform1i_t Uniform1i => _Uniform1i.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUniform1f_t(int location, float v0);
        private static readonly Lazy<glUniform1f_t> _Uniform1f = new Lazy<glUniform1f_t>(() => LoadFunction<glUniform1f_t>("glUniform1f"));
        public static glUniform1f_t Uniform1f => _Uniform1f.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUniform2f_t(int location, float v0, float v1);
        private static readonly Lazy<glUniform2f_t> _Uniform2f = new Lazy<glUniform2f_t>(() => LoadFunction<glUniform2f_t>("glUniform2f"));
        public static glUniform2f_t Uniform2f => _Uniform2f.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUniform3f_t(int location, float v0, float v1, float v2);
        private static readonly Lazy<glUniform3f_t> _Uniform3f = new Lazy<glUniform3f_t>(() => LoadFunction<glUniform3f_t>("glUniform3f"));
        public static glUniform3f_t Uniform3f => _Uniform3f.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUniform4f_t(int location, float v0, float v1, float v2, float v3);
        private static readonly Lazy<glUniform4f_t> _Uniform4f = new Lazy<glUniform4f_t>(() => LoadFunction<glUniform4f_t>("glUniform4f"));
        public static glUniform4f_t Uniform4f => _Uniform4f.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUniformMatrix4fv_t(int location, int count, bool transpose, float* value);
        private static readonly Lazy<glUniformMatrix4fv_t> _UniformMatrix4fv = new Lazy<glUniformMatrix4fv_t>(() => LoadFunction<glUniformMatrix4fv_t>("glUniformMatrix4fv"));
        public static glUniformMatrix4fv_t UniformMatrix4fv => _UniformMatrix4fv.Value;
        

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate uint glGetUniformBlockIndex_t(uint program, byte* uniformBlockName);
        private static readonly Lazy<glGetUniformBlockIndex_t> _GetUniformBlockIndex = new Lazy<glGetUniformBlockIndex_t>(() => LoadFunction<glGetUniformBlockIndex_t>("glGetUniformBlockIndex"));
        public static glGetUniformBlockIndex_t GetUniformBlockIndex => _GetUniformBlockIndex.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int glGetUniformLocation_t(uint program, byte* name);
        private static readonly Lazy<glGetUniformLocation_t> _GetUniformLocation = new Lazy<glGetUniformLocation_t>(
            () => LoadFunction<glGetUniformLocation_t>("glGetUniformLocation"));
        public static glGetUniformLocation_t GetUniformLocation => _GetUniformLocation.Value;



        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int glGetAttribLocation_t(uint program, byte* name);
        private static readonly Lazy<glGetAttribLocation_t> _GetAttribLocation = new Lazy<glGetAttribLocation_t>(() => LoadFunction<glGetAttribLocation_t>("glGetAttribLocation"));
        public static glGetAttribLocation_t GetAttribLocation => _GetAttribLocation.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glUseProgram_t(uint program);
        private static readonly Lazy<glUseProgram_t> _UseProgram = new Lazy<glUseProgram_t>(() => LoadFunction<glUseProgram_t>("glUseProgram"));
        public static glUseProgram_t UseProgram => _UseProgram.Value;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBufferData_t(BufferTarget target, UIntPtr size, void* data, BufferUsageHint usage);
        private static readonly Lazy<glBufferData_t> _BufferData = new Lazy<glBufferData_t>(() => LoadFunction<glBufferData_t>("glBufferData"));
        public static glBufferData_t BufferData => _BufferData.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glNamedBufferData_t(uint buffer, uint size, void* data, BufferUsageHint usage);
        private static readonly Lazy<glNamedBufferData_t> _NamedBufferData = new Lazy<glNamedBufferData_t>(() => LoadFunction<glNamedBufferData_t>("glNamedBufferData"));
        public static glNamedBufferData_t NamedBufferData => _NamedBufferData.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glTexImage2D_t(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            uint width,
            uint height,
            int border,
            GLPixelFormat format,
            GLPixelType type,
            void* data);
        private static readonly Lazy<glTexImage2D_t> _TexImage2D = new Lazy<glTexImage2D_t>(() => LoadFunction<glTexImage2D_t>("glTexImage2D"));
        public static glTexImage2D_t TexImage2D => _TexImage2D.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glEnableVertexAttribArray_t(uint index);
        private static readonly Lazy<glEnableVertexAttribArray_t> _EnableVertexAttribArray = new Lazy<glEnableVertexAttribArray_t>(() => LoadFunction<glEnableVertexAttribArray_t>("glEnableVertexAttribArray"));
        public static glEnableVertexAttribArray_t EnableVertexAttribArray => _EnableVertexAttribArray.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glDisableVertexAttribArray_t(uint index);
        private static readonly Lazy<glDisableVertexAttribArray_t> _DisableVertexAttribArray = new Lazy<glDisableVertexAttribArray_t>(() => LoadFunction<glDisableVertexAttribArray_t>("glDisableVertexAttribArray"));
        public static glDisableVertexAttribArray_t DisableVertexAttribArray => _DisableVertexAttribArray.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glVertexAttribPointer_t(
            uint index,
            int size,
            VertexAttribPointerType type,
            GLboolean normalized,
            uint stride,
            void* pointer);
        private static readonly Lazy<glVertexAttribPointer_t> _VertexAttribPointer = new Lazy<glVertexAttribPointer_t>(() => LoadFunction<glVertexAttribPointer_t>("glVertexAttribPointer"));
        public static glVertexAttribPointer_t VertexAttribPointer => _VertexAttribPointer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glVertexAttribIPointer_t(
            uint index,
            int size,
            VertexAttribPointerType type,
            uint stride,
            void* pointer);
        private static readonly Lazy<glVertexAttribIPointer_t> _VertexAttribIPointer = new Lazy<glVertexAttribIPointer_t>(() => LoadFunction<glVertexAttribIPointer_t>("glVertexAttribIPointer"));
        public static glVertexAttribIPointer_t VertexAttribIPointer => _VertexAttribIPointer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glVertexAttribDivisor_t(uint index, uint divisor);
        private static readonly Lazy<glVertexAttribDivisor_t> _VertexAttribDivisor = new Lazy<glVertexAttribDivisor_t>(() => LoadFunction<glVertexAttribDivisor_t>("glVertexAttribDivisor"));
        public static glVertexAttribDivisor_t VertexAttribDivisor => _VertexAttribDivisor.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glFrontFace_t(FrontFaceDirection mode);
        private static readonly Lazy<glFrontFace_t> _FrontFace = new Lazy<glFrontFace_t>(() => LoadFunction<glFrontFace_t>("glFrontFace"));
        public static glFrontFace_t FrontFace => _FrontFace.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetIntegerv_t(GetPName pname, int* data);
        private static readonly Lazy<glGetIntegerv_t> _GetIntegerv = new Lazy<glGetIntegerv_t>(() => LoadFunction<glGetIntegerv_t>("glGetIntegerv"));
        public static glGetIntegerv_t GetIntegerv => _GetIntegerv.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glBindTextureUnit_t(uint unit, uint texture);
        private static readonly Lazy<glBindTextureUnit_t> _BindTextureUnit = new Lazy<glBindTextureUnit_t>(() => LoadFunction<glBindTextureUnit_t>("glBindTextureUnit"));
        public static glBindTextureUnit_t BindTextureUnit => _BindTextureUnit.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glTexParameteri_t(TextureTarget target, TextureParameterName pname, int param);
        private static readonly Lazy<glTexParameteri_t> _TexParameteri = new Lazy<glTexParameteri_t>(() => LoadFunction<glTexParameteri_t>("glTexParameteri"));
        public static glTexParameteri_t TexParameteri => _TexParameteri.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate byte* glGetString_t(StringName name);
        private static readonly Lazy<glGetString_t> _GetString = new Lazy<glGetString_t>(() => LoadFunction<glGetString_t>("glGetString"));
        public static glGetString_t GetString => _GetString.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glTexImage2DMultisample_t(
            TextureTarget target,
            uint samples,
            PixelInternalFormat internalformat,
            uint width,
            uint height,
            GLboolean fixedsamplelocations);
        private static readonly Lazy<glTexImage2DMultisample_t> _TexImage2DMultisample = new Lazy<glTexImage2DMultisample_t>(() => LoadFunction<glTexImage2DMultisample_t>("glTexImage2DMultisample"));
        public static glTexImage2DMultisample_t TexImage2DMultisample => _TexImage2DMultisample.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glMapBuffer_t(BufferTarget target, BufferAccess access, void** mapped);
        private static readonly Lazy<glMapBuffer_t> _MapBuffer = new Lazy<glMapBuffer_t>(() => LoadFunction<glMapBuffer_t>("glMapBuffer"));
        public static glMapBuffer_t MapBuffer => _MapBuffer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glMapNamedBuffer_t(uint buffer, BufferAccess access, void** mapped);
        private static readonly Lazy<glMapNamedBuffer_t> _MapNamedBuffer = new Lazy<glMapNamedBuffer_t>(() => LoadFunction<glMapNamedBuffer_t>("glMapNamedBuffer"));
        public static glMapNamedBuffer_t MapNamedBuffer => _MapNamedBuffer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate GLboolean glUnmapBuffer_t(BufferTarget target);
        private static readonly Lazy<glUnmapBuffer_t> _UnmapBuffer = new Lazy<glUnmapBuffer_t>(() => LoadFunction<glUnmapBuffer_t>("glUnmapBuffer"));
        public static glUnmapBuffer_t UnmapBuffer => _UnmapBuffer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate GLboolean glUnmapNamedBuffer_t(uint buffer);
        private static readonly Lazy<glUnmapNamedBuffer_t> _UnmapNamedBuffer = new Lazy<glUnmapNamedBuffer_t>(() => LoadFunction<glUnmapNamedBuffer_t>("glUnmapNamedBuffer"));
        public static glUnmapNamedBuffer_t UnmapNamedBuffer => _UnmapNamedBuffer.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glCopyBufferSubData_t(
            BufferTarget readTarget,
            BufferTarget writeTarget,
            IntPtr readOffset,
            IntPtr writeOffset,
            IntPtr size);
        private static readonly Lazy<glCopyBufferSubData_t> _CopyBufferSubData = new Lazy<glCopyBufferSubData_t>(() => LoadFunction<glCopyBufferSubData_t>("glCopyBufferSubData"));
        public static glCopyBufferSubData_t CopyBufferSubData => _CopyBufferSubData.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetTexImage_t(
            TextureTarget target,
            int level,
            GLPixelFormat format,
            GLPixelType type,
            void* pixels);
        private static readonly Lazy<glGetTexImage_t> _GetTexImage = new Lazy<glGetTexImage_t>(() => LoadFunction<glGetTexImage_t>("glGetTexImage"));
        public static glGetTexImage_t GetTexImage => _GetTexImage.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetTextureSubImage_t(
            uint texture,
            int level,
            int xoffset,
            int yoffset,
            int zoffset,
            uint width,
            uint height,
            uint depth,
            GLPixelFormat format,
            GLPixelType type,
            uint bufSize,
            void* pixels);
        private static readonly Lazy<glGetTextureSubImage_t> _GetTextureSubImage = new Lazy<glGetTextureSubImage_t>(() => LoadFunction<glGetTextureSubImage_t>("glGetTextureSubImage"));
        public static glGetTextureSubImage_t GetTextureSubImage => _GetTextureSubImage.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glCopyNamedBufferSubData_t(
            uint readBuffer,
            uint writeBuffer,
            IntPtr readOffset,
            IntPtr writeOffset,
            uint size);
        private static readonly Lazy<glCopyNamedBufferSubData_t> _CopyNamedBufferSubData = new Lazy<glCopyNamedBufferSubData_t>(() => LoadFunction<glCopyNamedBufferSubData_t>("glCopyNamedBufferSubData"));
        public static glCopyNamedBufferSubData_t CopyNamedBufferSubData => _CopyNamedBufferSubData.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glCreateBuffers_t(uint n, uint* buffers);
        private static readonly Lazy<glCreateBuffers_t> _CreateBuffers = new Lazy<glCreateBuffers_t>(() => LoadFunction<glCreateBuffers_t>("glCreateBuffers"));
        public static glCreateBuffers_t CreateBuffers => _CreateBuffers.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glCreateTextures_t(TextureTarget target, uint n, uint* textures);
        private static readonly Lazy<glCreateTextures_t> _CreateTextures = new Lazy<glCreateTextures_t>(() => LoadFunction<glCreateTextures_t>("glCreateTextures"));
        public static glCreateTextures_t CreateTextures => _CreateTextures.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glStencilMask_t(uint mask);
        private static readonly Lazy<glStencilMask_t> _StencilMask = new Lazy<glStencilMask_t>(() => LoadFunction<glStencilMask_t>("glStencilMask"));
        public static glStencilMask_t StencilMask => _StencilMask.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glClearStencil_t(int s);
        private static readonly Lazy<glClearStencil_t> _ClearStencil = new Lazy<glClearStencil_t>(() => LoadFunction<glClearStencil_t>("glClearStencil"));
        public static glClearStencil_t ClearStencil => _ClearStencil.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGetActiveUniform_t(
            uint program,
            uint index,
            uint bufSize,
            uint* length,
            int* size,
            uint* type,
            byte* name);
        private static readonly Lazy<glGetActiveUniform_t> _GetActiveUniform = new Lazy<glGetActiveUniform_t>(() => LoadFunction<glGetActiveUniform_t>("glGetActiveUniform"));
        public static glGetActiveUniform_t GetActiveUniform => _GetActiveUniform.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGenerateMipmap_t(TextureTarget target);
        private static readonly Lazy<glGenerateMipmap_t> _GenerateMipmap = new Lazy<glGenerateMipmap_t>(() => LoadFunction<glGenerateMipmap_t>("glGenerateMipmap"));
        public static glGenerateMipmap_t GenerateMipmap => _GenerateMipmap.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glGenerateTextureMipmap_t(uint texture);
        private static readonly Lazy<glGenerateTextureMipmap_t> _GenerateTextureMipmap = new Lazy<glGenerateTextureMipmap_t>(() => LoadFunction<glGenerateTextureMipmap_t>("glGenerateTextureMipmap"));
        public static glGenerateTextureMipmap_t GenerateTextureMipmap => _GenerateTextureMipmap.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glFlush_t();
        private static readonly Lazy<glFlush_t> _Flush = new Lazy<glFlush_t>(() => LoadFunction<glFlush_t>("glFlush"));
        public static glFlush_t Flush => _Flush.Value;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void glFinish_t();
        private static readonly Lazy<glFinish_t> _Finish = new Lazy<glFinish_t>(() => LoadFunction<glFinish_t>("glFinish"));
        public static glFinish_t Finish => _Finish.Value;



        #region Helper for retriving OpenGL functions
        private static T LoadFunction<T>(string name) where T : Delegate
        {
            IntPtr proc = wglGetProcAddress(name);
            if (proc == IntPtr.Zero)
            {
                proc = GetProcAddressFromOpenGL32(name);
                if (proc == IntPtr.Zero)
                    throw new Exception($"Failed to load OpenGL function: {name}");
            }
            return Marshal.GetDelegateForFunctionPointer<T>(proc);
        }
        private static IntPtr GetProcAddressFromOpenGL32(string name)
        {
            IntPtr moduleHandle = GetModuleHandle("opengl32.dll");
            if (moduleHandle == IntPtr.Zero)
                throw new Exception("Failed to get module handle for opengl32.dll.");

            return GetProcAddress(moduleHandle, name);
        }
        #endregion
    }
}
