var webpack = require('webpack')

module.exports = {
	runtimeCompiler: true,

	chainWebpack(config){
		config.plugin('html')
			.tap(args => {
        args[0].title = 'DM插件&VKSP记录-Vico'
        args[0].basePath = 'DMPluginView'
				return args
			})
    config.plugin('Quill')
      .use(webpack.ProvidePlugin, [{
        'window.Quill': 'quill/dist/quill.js',
        'Quill': 'quill/dist/quill.js'
        //或者
        //'window.Quill': 'quill'
    }])
	},
    
  publicPath: './',

}
