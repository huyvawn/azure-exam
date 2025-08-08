import { useEffect, useState } from "react";
import axios from "axios";
const API = "https://localhost:5001"; // đổi theo backend

export default function App() {
  const [rows, setRows] = useState([]);
  const [form, setForm] = useState({
    playerName: "",
    fullName: "",
    age: 0,
    level: 0,
  });

  /* READ */
  const load = async () => {
    const { data } = await axios.get(`${API}/players`);
    setRows(data);
  };
  useEffect(load, []);

  /* CREATE/UPDATE */
  const save = async (e) => {
    e.preventDefault();
    await axios.post(`${API}/players`, form);
    setForm({ playerName: "", fullName: "", age: 0, level: 0 });
    load();
  };

  /* DELETE */
  const del = async (id) => {
    await axios.delete(`${API}/players/${id}`);
    load();
  };

  return (
    <div className="max-w-3xl mx-auto p-6">
      <h1 className="text-2xl font-bold mb-4">Battle Game Players</h1>

      <table className="w-full border mb-6">
        <thead className="bg-gray-100">
          <tr>
            <th>No</th>
            <th>Player name</th>
            <th>Level</th>
            <th>Age</th>
            <th>Asset name</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr key={r.playerId} className="text-center">
              <td>{i + 1}</td>
              <td>{r.playerName}</td>
              <td>{r.level}</td>
              <td>{r.age}</td>
              <td>{r.assets.join(", ")}</td>
              <td>
                <button
                  onClick={() => del(r.playerId)}
                  className="text-red-600"
                >
                  🗑
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Form    */}
      <form onSubmit={save} className="grid grid-cols-2 gap-4">
        {["playerName", "fullName", "age", "level"].map((k) => (
          <input
            key={k}
            className="border p-2"
            placeholder={k}
            value={form[k]}
            onChange={(e) => setForm({ ...form, [k]: e.target.value })}
          />
        ))}
        <button className="col-span-2 bg-blue-600 text-white py-2 rounded">
          Save
        </button>
      </form>
    </div>
  );
}
