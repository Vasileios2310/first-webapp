function confirmDelete(studentId) {
    if (confirm("Are you sure you want to delete this student?")) {
        window.local.href = "/Students/" + studentId + "/Delete"
    }
}