import ContaManager from './components/ContaManager'
import './App.css'

function App() {
  return (
    <div className="min-h-screen bg-gray-100 py-8 flex items-center justify-center">
      <div className="w-full p-6 bg-white rounded-lg shadow-xl">
        <h1 className="text-4xl font-extrabold text-gray-800 text-center mb-10">Banco Digital</h1>
        <ContaManager />
      </div>
    </div>
  )
}

export default App
