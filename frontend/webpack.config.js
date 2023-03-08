// Generated using webpack-cli https://github.com/webpack/webpack-cli
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const {
  DefinePlugin
} = require('webpack');


module.exports = (env, argv) => {

  const isProduction = argv.mode == 'production';


  const stylesHandler = isProduction ? MiniCssExtractPlugin.loader : 'style-loader';

  const config = {
    target: 'web',
    entry: {
      photoViewer: './src/photo-viewer.ts',
      photoViewerStyleSheet: './src/less/photoViewer.less',
      management: './src/management.ts',
      managementStyleSheet: './src/less/management.less'
    },
    output: {
      filename: "[name].[contenthash].js",
      path: path.resolve(__dirname, 'dist'),
    },
    devServer: {
      open: true,
      host: 'localhost',
      hot: false,
      liveReload: true
    },
    plugins: [
      new HtmlWebpackPlugin({
        template: 'photo-viewer.html',
        filename: 'photo-viewer.html',
        chunks: ['photoViewerStyleSheet', 'photoViewer' ]
      }),
  
      new HtmlWebpackPlugin({
        template: 'photo-viewer.html',
        filename: 'index.html',
        chunks: ['photoViewerStyleSheet', 'photoViewer']
      }),
  
      new HtmlWebpackPlugin({
        template: 'management.html',
        filename: 'management.html',
        chunks: ['management', 'managementStyleSheet']
      }),
  
      new DefinePlugin({
        DEBUG: !isProduction,
        BASE_URL: process.env.BASE_URL
      }) 
  
      // Add your plugins here
      // Learn more about plugins from https://webpack.js.org/configuration/plugins/
    ],
    module: {
      rules: [
        {
          test: /\.(ts|tsx)$/i,
          loader: 'ts-loader',
          exclude: ['/node_modules/'],
        },
        {
          test: /\.less$/i,
          use: [stylesHandler, 'css-loader', 'postcss-loader', 'less-loader'],
        },
        {
          test: /\.css$/i,
          use: [stylesHandler, 'css-loader', 'postcss-loader'],
        },
        {
          test: /\.(eot|svg|ttf|woff|woff2|png|jpg|gif)$/i,
          type: 'asset',
        },
        {
          test: /\.vue$/,
          loader: 'vue-loader'
        }
  
        // Add your rules for custom modules here
        // Learn more about loaders from https://webpack.js.org/loaders/
      ],
    },
    resolve: {
      extensions: ['.tsx', '.ts', '.jsx', '.js', '...'],
    },
  };
  
  if (isProduction) {

    config.plugins.push(new MiniCssExtractPlugin());

  }
  config.mode = argv.mode;
  return config;
};
