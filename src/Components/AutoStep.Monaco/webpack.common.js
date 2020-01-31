const path = require('path');
const MonacoWebpackPlugin = require('monaco-editor-webpack-plugin');

module.exports = {
  entry: {
     app: "./MonacoInterop.ts",
    "editor.worker": "monaco-editor/esm/vs/editor/editor.worker.js"
  },
  resolve: {
    extensions: [".ts", ".js"]
  },
  output: {
    globalObject: "self",
    filename: "[name].bundle.js",
    path: path.resolve(__dirname, 'wwwroot')
  },
  module: {
    rules: [
    {
        test: /\.ts?$/,
        use: "ts-loader",
        exclude: /node_modules/
    },
    {
      test: /\.css$/,
      use: ['style-loader', 'css-loader']
    }, 
    {
      test: /\.ttf$/,
      loader: 'file-loader',
      options: {
          publicPath: "/_content/AutoStep.Monaco"
      }
    }]
  },
  plugins: [
    new MonacoWebpackPlugin({publicPath: '/_content/AutoStep.Monaco/', languages: []})
  ]
};