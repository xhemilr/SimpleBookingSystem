import { useEffect, useState } from 'react';
import './App.css';
import Modal from './Modal';
import axios from 'axios';

interface Resource {
    id: number;
    name: string;
}

function App() {
    const [resources, setResources] = useState<Resource[]>();
    const [showModal, setShowModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState({ message: '', success: false });
    const [formData, setFormData] = useState({
        dateFrom: '',
        dateTo: '',
        quantity: '',
        resourceId: ''
    });

    const [errors, setErrors] = useState({});

    const handleChange = (e: { target: { name: any; value: any; }; }) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value,
        });
    };

    const validateForm = () => {
        const newErrors = {};

        if (isNaN(+formData.quantity) || parseInt(formData.quantity) < 1) {
            newErrors.quantity = 'Quantity cannot be less than 1!';
        }

        var dateFrom = new Date(formData.dateFrom);

        if (isNaN(dateFrom)) {
            newErrors.dateFrom = 'Date from cannot be null!';
        } else if (dateFrom < new Date()) {
            newErrors.dateFrom = 'Resource cannot be booked in the past!';
        }

        var dateTo = new Date(formData.dateTo);

        if (isNaN(dateTo)) {
            newErrors.dateTo = 'Date to cannot be null!';
        } else if (dateTo < dateFrom) {
            newErrors.dateTo = 'Date to must be greater than date from!';
        }

        setErrors(newErrors);

        return Object.keys(newErrors).length === 0;
    };

    useEffect(() => {
        populateResourceData();
    }, []);

    const openModal = (id: string) => {
        setFormData((prevData) => ({ ...prevData, resourceId: id }));
        setShowModal(true);
    };

    const closeModal = () => {
        setShowModal(false);
    };

    const handleSubmit = async (e: { preventDefault: () => void }) => {
        e.preventDefault();

        const isValid = validateForm();

        if (isValid) {
            const response = await axios.post('https://localhost:7195/api/Booking/BookResource', formData)
            setSuccessMessage({
                message: response.data.messages[0],
                success: response.data.succeeded
            });

            setShowModal(false);
        } else {
            console.log('Form has validation errors');
        }
        
    };

    const contents =
        resources === undefined ? (
            <p>
                <em>Loading... Please refresh once the ASP.NET backend has started.</em>
            </p>
        ) : (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {resources.map((resource) => (
                        <tr key={resource.id}>
                            <td>{resource.id}</td>
                            <td>{resource.name}</td>
                            <td>
                                <button onClick={() => openModal(resource.id)}>Book here</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );

    return (
        <div>
            {successMessage.success && <div style={{ color: 'green' }}><strong>{successMessage.message}</strong></div>}
                {!successMessage.success && <div style={{ color: 'red' }}><strong>{successMessage.message}</strong></div>}
            <h1 id="tableLabel">Resources</h1>
            {contents}
            <Modal isOpen={showModal} onClose={closeModal}>
                <h2>Book resource</h2>
                <form onSubmit={handleSubmit}>
                    <label>Date From:</label>
                    <input
                        type="datetime-local"
                        name="dateFrom"
                        value={formData.dateFrom}
                        onChange={handleChange}
                    />
                    {errors.dateFrom && <p style={{ color: 'red' }}>{errors.dateFrom}</p>}
                    <br />

                    <label>Date To:</label>
                    <input
                        type="datetime-local"
                        name="dateTo"
                        value={formData.dateTo}
                        onChange={handleChange}
                    />
                    {errors.dateTo && <p style={{ color: 'red' }}>{errors.dateTo}</p>}
                    <br />

                    <label>Quantity:</label>
                    <input
                        type="number"
                        name="quantity"
                        value={formData.quantity}
                        onChange={handleChange}
                    />
                    {errors.quantity && <p style={{ color: 'red' }}>{errors.quantity}</p>}
                    <br />

                    <button type="submit">Submit</button>
                </form>
            </Modal>
        </div>
    );

    async function populateResourceData() {
        try {
            const response = await fetch(
                "https://localhost:7195/api/Resource/GetAllResources"
            );
            const data = await response.json();
            if (data.succeeded) {
                setResources(data.data);
            } else {
                console.error("Failed to fetch data");
            }
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    }
}
export default App;